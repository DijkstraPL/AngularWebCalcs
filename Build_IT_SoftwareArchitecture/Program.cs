using Structurizr;
using Structurizr.Api;

namespace Build_IT_SoftwareArchitecture
{
    public class Program
    {        
        private const long WorkspaceId = 47214;
        private const string ApiKey = "9c77f434-938c-40c9-b38d-d9011f2da27b";
        private const string ApiSecret = "5198d30d-3b12-4d6e-8128-b73573ef42de";
        private static Workspace _workspace;
        private static SoftwareSystem _softwareSystem;

        /// <summary>
        /// https://github.com/structurizr/dotnet/blob/master/docs/getting-started.md
        /// </summary>
        public static void Main()
        {
            _workspace = new Workspace("Building-IT", "This is a model of Building-IT software system.");
            CreateSoftwareSystem();
            var diagramsCreator = new DiagramsCreator(_workspace, _softwareSystem);
            diagramsCreator.CreateDiagrams();

            var documentationCreator = new DocumentationCreator(_workspace, _softwareSystem);
            documentationCreator.CreateDocumentation();

            SendWorkspaceToServer();
        }

        private static void CreateSoftwareSystem()
        {
            Model model = _workspace.Model;

            _softwareSystem = model.AddSoftwareSystem("Build-IT System", "System for all software related to Building-IT");
        }

        private static void SendWorkspaceToServer()
        {
            var structurizrClient = new StructurizrClient(ApiKey, ApiSecret);
            structurizrClient.PutWorkspace(WorkspaceId, _workspace);
        }
    }
}
