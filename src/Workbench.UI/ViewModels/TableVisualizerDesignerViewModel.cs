using System;
using System.Diagnostics.Contracts;
using Caliburn.Micro;
using Workbench.Core.Models;
using Workbench.Services;

namespace Workbench.ViewModels
{
    public class TableVisualizerDesignerViewModel : EditorViewModel
    {
        private TableViewModel grid;

        public TableVisualizerDesignerViewModel(TableVisualizerModel theTableModel,
                                               IEventAggregator theEventAggregator,
                                               IDataService theDataService,
                                               IViewModelService theViewModelService)
            : base(theTableModel, theEventAggregator, theDataService, theViewModelService)
        {
            Contract.Requires<ArgumentNullException>(theTableModel != null);
            Contract.Requires<ArgumentNullException>(theEventAggregator != null);
            Contract.Requires<ArgumentNullException>(theDataService != null);
            Contract.Requires<ArgumentNullException>(theViewModelService != null);

            Model = theTableModel;
            Grid = new TableViewModel(theTableModel.Table);
        }

        /// <summary>
        /// Gets or sets the map view model.
        /// </summary>
        public TableViewModel Grid
        {
            get { return this.grid; }
            set
            {
                this.grid = value;
                NotifyOfPropertyChange();
            }
        }

        public void AddColumn(TableColumnModel newColumn)
        {
            Contract.Requires<ArgumentNullException>(newColumn != null);
            Grid.AddColumn(newColumn);
        }

        public void Resize(int columns, int rows)
        {
            Contract.Assume(Grid != null);
            Grid.Resize(columns, rows);
        }

        public void AddRow(TableRowModel newRow)
        {
            Contract.Requires<ArgumentNullException>(newRow != null);
            Contract.Assume(Grid != null);
            Grid.AddRow(newRow);
        }
    }
}
