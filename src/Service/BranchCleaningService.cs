using Stag.SourceControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Stag.Service
{
    public class BranchCleaningService
    {
        private Git _git;

        private const string MasterBranchName = "master";
        private const string OsBranchPattern = "os-[0-9]{4}-0[0-9]{2}";
        private const string MaintenanceBranchPattern = "sde-[0-9]-[0-9]-[0-9]";
        private const string MaintenanceWeeklyBranchPattern = "sde-[0-9]{3}-[1-3][0-9]";

        public BranchCleaningService()
            : this(new Git())
        {
        }

        public BranchCleaningService(Git git)
        {
            _git = git;
        }

        public IList<string> GetLocalBranches()
        {
            return _git.GetAllBranches()
                .Where(p => !p.IsRemote)
                .Select(p => p.Name)
                .ToList();
        }

        public IList<string> GetNonOficialBranches()
        {
            return this.GetLocalBranches()
                .Where(p => this.IsNonOficialBranch(p))
                .ToList();
        }

        public void CleanBranches(IList<string> branches)
        {
            foreach (var branch in branches)
            {
                _git.DeleteBranch(branch);
            }
        }

        private bool IsNonOficialBranch(string branchName)
        {
            var isMasterBranch = string.Equals(branchName, MasterBranchName, StringComparison.OrdinalIgnoreCase);
            var isOsBranch = Regex.IsMatch(branchName, OsBranchPattern);
            var isMaintenanceBranch = Regex.IsMatch(branchName, MaintenanceBranchPattern);
            var isMaintenanceWeeklyBranch = Regex.IsMatch(branchName, MaintenanceWeeklyBranchPattern);

            return !isMasterBranch
                && !isOsBranch
                && !isMaintenanceBranch
                && !isMaintenanceWeeklyBranch;
        }
    }
}