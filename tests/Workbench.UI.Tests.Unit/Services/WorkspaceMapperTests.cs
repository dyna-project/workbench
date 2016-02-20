﻿using Caliburn.Micro;
using Workbench.Services;
using Moq;
using NUnit.Framework;
using Workbench.Core.Models;
using Workbench.ViewModels;

namespace Workbench.UI.Tests.Unit.Services
{
    [TestFixture]
    public class WorkspaceMapperTests
    {
        private IViewModelService _viewModelService;

        [SetUp]
        public void Initialize()
        {
            this._viewModelService = new ViewModelService();
        }

        [Test]
        public void MapFrom_With_Valid_Model_Returns_Expected_Variables()
        {
            var sut = CreateSut();
            var actualWorkspaceModel = sut.MapFrom(WorkspaceModelFactory.Create());
            Assert.That(actualWorkspaceModel.Model.Variables.Count, Is.EqualTo(2));
        }

        [Test]
        public void MapFrom_With_Valid_Model_Returns_Expected_Domains()
        {
            var sut = CreateSut();
            var actualWorkspaceModel = sut.MapFrom(WorkspaceModelFactory.Create());
            Assert.That(actualWorkspaceModel.Model.Domains.Count, Is.EqualTo(1));
        }

        [Test]
        public void MapFrom_With_Valid_Model_Returns_Expected_Constraints()
        {
            var sut = CreateSut();
            var actualWorkspaceModel = sut.MapFrom(WorkspaceModelFactory.Create());
            Assert.That(actualWorkspaceModel.Model.Constraints.Count, Is.EqualTo(1));
        }

        [Test]
        public void MapFrom_With_Valid_Model_Sets_Expected_Workspace_Model()
        {
            var sut = CreateSut();
            var actualWorkspaceModel = sut.MapFrom(WorkspaceModelFactory.Create());
            Assert.That(actualWorkspaceModel.WorkspaceModel, Is.Not.Null);
        }

        private WorkspaceMapper CreateSut()
        {
            return new WorkspaceMapper(CreateModelMapper(),
                                       CreateSolutionMapper(),
                                       CreateDisplayMapper(),
                                       CreateViewModelFactoryMock().Object);
        }

        private Mock<IViewModelFactory> CreateViewModelFactoryMock()
        {
            var mock = new Mock<IViewModelFactory>();
            mock.Setup(_ => _.CreateWorkspace())
                .Returns(new WorkspaceViewModel(CreateDataService(),
                                                CreateWindowManager(),
                                                CreateEventAggregator(),
                                                this._viewModelService));

            return mock;
        }

        private DisplayMapper CreateDisplayMapper()
        {
            return new DisplayMapper(CreateEventAggregator(),
                                     CreateDataService(),
                                     this._viewModelService);
        }

        private SolutionMapper CreateSolutionMapper()
        {
            return new SolutionMapper(this._viewModelService,
                                      CreateEventAggregator());
        }

        private ModelMapper CreateModelMapper()
        {
            return new ModelMapper(CreateVariableMapper(),
                                   CreateConstraintMapper(),
                                   CreateDomainMapper(),
                                   CreateWindowManager(),
                                   CreateEventAggregator());
        }

        private DomainMapper CreateDomainMapper()
        {
            return new DomainMapper(this._viewModelService);
        }

        private ConstraintMapper CreateConstraintMapper()
        {
            return new ConstraintMapper(this._viewModelService);
        }

        private VariableMapper CreateVariableMapper()
        {
            return new VariableMapper(this._viewModelService,
                                      CreateEventAggregator());
        }

        private static IDataService CreateDataService()
        {
            var mock = new Mock<IDataService>();
            mock.Setup(_ => _.GetWorkspace())
                .Returns(new WorkspaceModel());
            return mock.Object;
        }

        private static IEventAggregator CreateEventAggregator()
        {
            return new Mock<IEventAggregator>().Object;
        }

        private static IWindowManager CreateWindowManager()
        {
            return Mock.Of<IWindowManager>();
        }
    }
}
