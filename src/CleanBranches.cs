using Stag.Configuration;
using Stag.Service;
using Stag.SourceControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Stag
{
    public partial class CleanBranches : Form
    {
        private readonly BranchCleaningService _branchCleaningService;

        internal CleanBranches(ISettings settings)
            : this(new BranchCleaningService(new Git(settings)))
        {
        }

        public CleanBranches(BranchCleaningService branchCleaningService)
        {
            _branchCleaningService = branchCleaningService;

            InitializeComponent();
        }

        private void LoadBranchesList()
        {
            lstBranches.Items.Clear();

            var localBranches = _branchCleaningService.GetLocalBranches();
            var nonOficialBranches = _branchCleaningService.GetNonOficialBranches();

            lstBranches.Items.AddRange(localBranches.ToArray());

            for (int i = 0; i < lstBranches.Items.Count; i++)
            {
                var checkItem = nonOficialBranches.Contains(lstBranches.Items[i]);
                lstBranches.SetItemChecked(i, checkItem);
            }
        }

        private void CleanBranches_Load(object sender, EventArgs e)
        {
            this.LoadBranchesList();
        }

        private void btnCleanBranches_Click(object sender, EventArgs e)
        {
            CleanSelectedBranches();

            LoadBranchesList();

            MessageBox.Show("Limpeza de branches efetuada com sucesso!");

            this.Close();
        }

        private void CleanSelectedBranches()
        {
            var branchesToClean = new List<string>();

            foreach (var branch in lstBranches.CheckedItems)
            {
                branchesToClean.Add(branch.ToString());
            }

            _branchCleaningService.CleanBranches(branchesToClean);
        }
    }
}