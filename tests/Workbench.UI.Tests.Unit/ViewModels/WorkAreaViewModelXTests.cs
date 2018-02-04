﻿using System;
using Caliburn.Micro;
using Workbench.ViewModels;
using Moq;
using NUnit.Framework;
using Workbench.Core.Models;
using Workbench.Messages;
using Workbench.Services;

namespace Workbench.UI.Tests.Unit.ViewModels
{
    [TestFixture]
    public class WorkAreaViewModelXTests
    {
        private Mock<IEventAggregator> eventAggregatorMock;
        private IViewModelService viewModelService;
        private Mock<IViewModelFactory> viewModelFactoryMock;
        private Mock<IWindowManager> windowManagerMock;

        [SetUp]
        public void Initialize()
        {
            this.eventAggregatorMock = new Mock<IEventAggregator>();
            this.viewModelFactoryMock = CreateViewModelFactoryMock();
            this.viewModelService = Mock.Of<IViewModelService>();
            this.windowManagerMock = CreateWindowManagerMock();
        }

        [Test]
        public void SolveWithValidModelReturnsSuccessStatus()
        {
            var sut = CreateValidWorkArea();
            var actualStatus = sut.SolveModel();
            Assert.That(actualStatus.IsSuccess, Is.True);
        }

        [Test]
        public void AddWithValidSingletonVariablePublishesVariableAddedMessage()
        {
            var sut = CreateValidWorkArea();
            var x = new SingletonVariableModel(sut.WorkspaceModel.Model, new ModelName("z"));
            sut.AddSingletonVariable(new SingletonVariableViewModel(new SingletonVariableGraphicModel(x), this.eventAggregatorMock.Object));
            this.eventAggregatorMock.Verify(_ => _.Publish(It.Is<SingletonVariableAddedMessage>(msg => msg.NewVariableName == "z"), It.IsAny<Action<System.Action>>()),
                                            Times.Once);
        }

        [Test]
        public void AddWithValidAggregatorVariablePublishesVariableAddedMessage()
        {
            var sut = CreateValidWorkArea();
            var x = new AggregateVariableModel(sut.WorkspaceModel.Model, new ModelName("z"));
            sut.AddAggregateVariable(new AggregateVariableViewModel(new AggregateVariableGraphicModel(x), this.eventAggregatorMock.Object));
            this.eventAggregatorMock.Verify(_ => _.Publish(It.Is<AggregateVariableAddedMessage>(msg => msg.NewVariableName == "z"), It.IsAny<Action<System.Action>>()),
                                            Times.Once);
        }

        [Test]
        public void DeleteWithValidVariablePublishesVariableDeletedMessage()
        {
            var sut = CreateValidWorkArea();
            var variableToDelete = sut.Editor.GetVariableByName("x");
            sut.DeleteVariable(variableToDelete);
            this.eventAggregatorMock.Verify(_ => _.Publish(It.Is<VariableDeletedMessage>(msg => msg.VariableName == "x"), It.IsAny<Action<System.Action>>()),
                                            Times.Once);
        }

        private WorkAreaViewModel CreateValidWorkArea()
        {
            var workspaceViewModel = new WorkAreaViewModel(CreateDataService(),
                                                            CreateWindowManagerMock().Object,
                                                            this.eventAggregatorMock.Object,
                                                            this.viewModelService,
                                                            this.viewModelFactoryMock.Object);
            workspaceViewModel.AddSingletonVariable(new SingletonVariableViewModel(new SingletonVariableGraphicModel(new SingletonVariableModel(workspaceViewModel.WorkspaceModel.Model, new ModelName("x"), new VariableDomainExpressionModel("1..10"))), Mock.Of<IEventAggregator>()));
            workspaceViewModel.AddAggregateVariable(new AggregateVariableViewModel(new AggregateVariableGraphicModel(new AggregateVariableModel(workspaceViewModel.WorkspaceModel.Model, new ModelName("y"), 2, new VariableDomainExpressionModel("1..10"))), Mock.Of<IEventAggregator>()));
            workspaceViewModel.AddExpressionConstraint(new ExpressionConstraintViewModel(new ExpressionConstraintGraphicModel(new ExpressionConstraintModel(new ConstraintExpressionModel("$x > 1")))));
            workspaceViewModel.AddExpressionConstraint(new ExpressionConstraintViewModel(new ExpressionConstraintGraphicModel(new ExpressionConstraintModel(new ConstraintExpressionModel("$y[0] <> $y[1]")))));

            return workspaceViewModel;
        }

        private IDataService CreateDataService()
        {
            return new DataService(CreateWorkspaceReaderWriterMock().Object);
        }

        private Mock<IWorkspaceReaderWriter> CreateWorkspaceReaderWriterMock()
        {
            return new Mock<IWorkspaceReaderWriter>();
        }

        private static Mock<IWindowManager> CreateWindowManagerMock()
        {
            return new Mock<IWindowManager>();
        }

        private Mock<IViewModelFactory> CreateViewModelFactoryMock()
        {
            return new Mock<IViewModelFactory>();
        }
    }
}
