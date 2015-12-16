namespace Stag.Tasks
{
    public class Task
    {
        public Task(string title, string id)
        {
            this.Id = id;
            this.Title = title;
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string DevelopmentBranchName { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.Id, this.Title);
        }
    }
}