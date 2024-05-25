using Structurizr;
using Structurizr.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_SoftwareArchitecture
{
    internal class DiagramsCreator
    {
        private readonly Workspace _workspace;
        private readonly SoftwareSystem _softwareSystem;
        private readonly Model _model;

        private Person _user;

        private Container _webApplication;
        private Container _singlePageApplication;
        private Container _apiApplication;
        private Container _database;
        private Container _mobileApp;
        private Container _desktopApp;

        public DiagramsCreator(Workspace workspace, SoftwareSystem softwareSystem)
        {
            _workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));
            _softwareSystem = softwareSystem ?? throw new ArgumentNullException(nameof(softwareSystem));

            _model = _workspace.Model;
        }

        internal void CreateDiagrams()
        {
            CreateSystemContext();
            SetContainerViewForSystem();
            SetComponentViewForApiApplication();
            SetStyles();
            SetViews();
        }

        private void CreateSystemContext()
        {
            _user = _model.AddPerson("User", "A user of my software system.");
            _user.Uses(_softwareSystem, "Uses");
        }

        private void SetContainerViewForSystem()
        {

            _webApplication = _softwareSystem.AddContainer("Web Application", "Delivers the static content and the internet building it single page application", "C#, ASP.NET CORE MVC");
            _user.Uses(_webApplication, "Visit building-it.net using", "HTTP/2");

            _singlePageApplication = _softwareSystem.AddContainer("Single-Page Application", "Provides all the internet calculation functionalities to users via their browser", "Javascript, Angular 2+");
            _user.Uses(_singlePageApplication, "Calculations, scripts, creation");
            _webApplication.Uses(_singlePageApplication, "Delivers to the customer's web browser");

            _mobileApp = _softwareSystem.AddContainer("Mobile Application", "Provides a limited subset of the Building-IT functionality to customers via their mobile device.", "Xamarin");
            _user.Uses(_mobileApp, "Calculations, scripts, creation");
            
            _desktopApp = _softwareSystem.AddContainer("Desktop Application", "Provides a limited subset of the Building-IT functionality to customers via desktop application.", "WPF");
            _user.Uses(_desktopApp, "Calculations, scripts, creation");

            _apiApplication = _softwareSystem.AddContainer("API Application", "Provides civil engineering calculation functionality via a JSON/HTTP API.", "C#");
            _singlePageApplication.Uses(_apiApplication, "Makes API calls to", "JSON/HTTP/2");
            _mobileApp.Uses(_apiApplication, "Makes API calls to", "JSON/HTTP/2");
            _desktopApp.Uses(_apiApplication, "Makes API calls to", "JSON/HTTP/2");

            _database = _softwareSystem.AddContainer("Database", "Stores registration information, hashed authentication credentials, materials and sections data, scripts and translations", "T-SQL");
            _apiApplication.Uses(_database, "Reads from and writes to", "EntityFramework/SQL");
        }

        private void SetComponentViewForApiApplication()
        {
            Component scriptController = _apiApplication.AddComponent("Script Controller", "Allows user to do some civil engineering calculations.", "C#, ASP.NET Core MVC Controller");
            Component applicationController = _apiApplication.AddComponent("Application Controller", "Allows user to create account, log in or reset their passsword.", "C#, ASP.NET Core MVC Controller");
            Component dataController = _apiApplication.AddComponent("Data Controller", "Provides to the users some business data from the database", "C#, ASP.NET Core MVC Controller");
            Component scriptInterpreterComponent = _apiApplication.AddComponent("Script Interpreter Component", "Allows to calculate loads on the construction e.g. wind load.", "C#");
            Component staticaComponent = _apiApplication.AddComponent("Statica Component", "Provides possibility to calculate statically indeterminated elements e.g. beams, frames etc.", "C#");
            Component loadsComponent = _apiApplication.AddComponent("Loads Component", "Allows to calculate loads on the construction e.g. wind load.", "C#");
            Component dataComponent = _apiApplication.AddComponent("Data Component", "Allows to get data from database e.g. material data.", "C#");

            _singlePageApplication.Uses(scriptController, "Makes API calls to", "JSON/HTTP/2");
            _singlePageApplication.Uses(applicationController, "Makes API calls to", "JSON/HTTP/2");
            _singlePageApplication.Uses(dataController, "Makes API calls to", "JSON/HTTP/2");
            scriptController.Uses(scriptInterpreterComponent, "Uses");
            scriptController.Uses(staticaComponent, "Uses");
            scriptController.Uses(loadsComponent, "Uses");
            scriptController.Uses(dataComponent, "Uses");
            applicationController.Uses(dataComponent, "Uses");
            dataController.Uses(dataComponent, "Uses");
            dataComponent.Uses(_database, "Reads from and writes to", "EntityFramework/SQL");
        }

        private void SetViews()
        {
            ViewSet viewSet = _workspace.Views;

            SystemContextView contextView = viewSet.CreateSystemContextView(_softwareSystem, "System Context", "System Context diagram for a Build-IT System");
            
            contextView.PaperSize = PaperSize.A5_Portrait;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            ContainerView containerView = viewSet.CreateContainerView(_softwareSystem, "Build-IT System", "Container diagram for System Context");
            containerView.PaperSize = PaperSize.A4_Landscape;
            containerView.AddAllElements();

            ComponentView componentViewForApiApplication = viewSet.CreateComponentView(_apiApplication, "API Application", "Component diagram for API Application");
            componentViewForApiApplication.PaperSize = PaperSize.A3_Landscape;
            componentViewForApiApplication.AddAllElements();
            componentViewForApiApplication.Remove(_user);
            componentViewForApiApplication.Remove(_webApplication);
        }

        private void SetStyles()
        {
            Styles styles = _workspace.Views.Configuration.Styles;
            _singlePageApplication.AddTags("Web app");
            _desktopApp.AddTags("Desktop app");
            _mobileApp.AddTags("Mobile app");
            _database.AddTags("Database");
            styles.Add(new ElementStyle(Tags.SoftwareSystem) { Background = "#1168bd", Color = "#ffffff" });
            styles.Add(new ElementStyle(Tags.Person) { Background = "#08427b", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle(Tags.Container) { Width = 650, Height = 400, Background = "#2279ce", Shape = Shape.Box });
            styles.Add(new ElementStyle("Web app") { Width = 550, Background = "#2279ce", Shape = Shape.WebBrowser });
            styles.Add(new ElementStyle("Desktop app") { Width = 550, Background = "#2279ce", Shape = Shape.WebBrowser });
            styles.Add(new ElementStyle("Mobile app") { Width = 550, Background = "#2279ce", Shape = Shape.MobileDeviceLandscape });
            styles.Add(new ElementStyle("Database") { Width = 550, Background = "#2279ce", Shape = Shape.Ellipse });
            styles.Add(new ElementStyle(Tags.Component) { Width = 550, Background = "#338adf", Shape = Shape.Box });
            styles.Add(new RelationshipStyle(Tags.Relationship) { Thickness = 4, Dashed = false, FontSize = 32, Width = 400 });
         
        }
    }
}
