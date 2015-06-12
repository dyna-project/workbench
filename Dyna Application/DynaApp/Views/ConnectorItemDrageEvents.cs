﻿using System.Windows;

namespace DynaApp.Views
{
    /// <summary>
    /// Arguments for event raised when the user starts to drag a connector out from a variable.
    /// </summary>
    internal class ConnectorItemDragStartedEventArgs : RoutedEventArgs
    {
        internal ConnectorItemDragStartedEventArgs(RoutedEvent routedEvent, object source) :
            base(routedEvent, source)
        {
        }

        /// <summary>
        /// Cancel dragging out of the connector.
        /// </summary>
        public bool Cancel
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Defines the event handler for ConnectorDragStarted events.
    /// </summary>
    internal delegate void ConnectorItemDragStartedEventHandler(object sender, ConnectorItemDragStartedEventArgs e);

    /// <summary>
    /// Arguments for event raised while user is dragging a variable in the network.
    /// </summary>
    internal class ConnectorItemDraggingEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// The amount the connector has been dragged horizontally.
        /// </summary>
        private double horizontalChange;

        /// <summary>
        /// The amount the connector has been dragged vertically.
        /// </summary>
        private double verticalChange;

        public ConnectorItemDraggingEventArgs(RoutedEvent routedEvent, object source, double horizontalChange, double verticalChange) :
            base(routedEvent, source)
        {
            this.horizontalChange = horizontalChange;
            this.verticalChange = verticalChange;
        }

        /// <summary>
        /// The amount the variable has been dragged horizontally.
        /// </summary>
        public double HorizontalChange
        {
            get
            {
                return horizontalChange;
            }
        }

        /// <summary>
        /// The amount the variable has been dragged vertically.
        /// </summary>
        public double VerticalChange
        {
            get
            {
                return verticalChange;
            }
        }
    }

    /// <summary>
    /// Defines the event handler for ConnectorDragStarted events.
    /// </summary>
    internal delegate void ConnectorItemDraggingEventHandler(object sender, ConnectorItemDraggingEventArgs e);

    /// <summary>
    /// Arguments for event raised when the user has completed dragging a connector.
    /// </summary>
    internal class ConnectorItemDragCompletedEventArgs : RoutedEventArgs
    {
        public ConnectorItemDragCompletedEventArgs(RoutedEvent routedEvent, object source) :
            base(routedEvent, source)
        {
        }
    }

    /// <summary>
    /// Defines the event handler for ConnectorDragCompleted events.
    /// </summary>
    internal delegate void ConnectorItemDragCompletedEventHandler(object sender, ConnectorItemDragCompletedEventArgs e);
}
