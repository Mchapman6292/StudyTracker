

namespace CodingTracker.View.FormService.LayoutServices
{
    public interface ILayoutService
    {
        IDisposable SuspendLayout(DataGridView gridView);
    }

    internal class LayoutService : ILayoutService
    {
        public IDisposable SuspendLayout(DataGridView gridView)
        {
            return new GridViewLayoutScope(gridView);
        }

        private class GridViewLayoutScope : IDisposable
        {
            private readonly DataGridView _gridView;

            public GridViewLayoutScope(DataGridView gridView)
            {
                _gridView = gridView;
                _gridView.SuspendLayout();
            }

            public void Dispose()
            {
                _gridView.ResumeLayout(true);
            }
        }
    }
}

