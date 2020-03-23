using System;
using System.Collections.Generic;
using System.Text;

namespace NuniToolbox.Ui.ViewModel
{
    /// <summary>
    /// A view model object for a window managing the view model for its content.
    /// </summary>
    public class WindowViewModel : ViewModelObject
    {
        private ViewModelObject m_currentViewModel;
        private bool m_disposeOldViewModel;

        /// <summary>
        /// The current view model for the window content.
        /// </summary>
        public ViewModelObject CurrentViewModel
        {
            get
            {
                return m_currentViewModel;
            }

            set
            {
                SetCurrentViewModel(value, m_disposeOldViewModel);
            }
        }

        /// <summary>
        /// True to dispose the old view model on replacing it.
        /// </summary>
        public bool DisposeOldViewModel
        {
            get
            {
                return m_disposeOldViewModel;
            }

            set
            {
                m_disposeOldViewModel = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Creates a new <see cref="WindowViewModel" />.
        /// </summary>
        /// <param name="viewModel">The current view model for the window content</param>
        /// <param name="disposeOldViewModel">True to dispose the old view model on replacing it</param>
        public WindowViewModel(ViewModelObject viewModel = null, bool disposeOldViewModel = true)
            : base()
        {
            m_currentViewModel = viewModel;
            m_disposeOldViewModel = disposeOldViewModel;
        }

        /// <summary>
        /// Sets the current view model for the window content.
        /// </summary>
        /// <param name="newViewModel">The new view model for the content</param>
        /// <param name="disposeOldViewModel">True to dispose the old view model</param>
        public void SetCurrentViewModel(ViewModelObject newViewModel, bool disposeOldViewModel)
        {
            if (newViewModel != m_currentViewModel)
            {
                if (disposeOldViewModel && m_currentViewModel != null)
                {
                    m_currentViewModel.Dispose();
                }

                m_currentViewModel = newViewModel;

                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
    }
}
