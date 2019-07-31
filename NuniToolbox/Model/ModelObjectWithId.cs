using System;
using System.Collections.Generic;
using System.Text;

namespace NuniToolbox.Model
{
    /// <summary>
    /// A more complex model object with a generic ID.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelObjectWithId<T> : ModelObject
    {
        private T m_id;

        /// <summary>
        /// The ID if the object
        /// </summary>
        public T Id
        {
            get
            {
                return m_id;
            }

            set
            {
                m_id = value;

                OnPropertyChanged();
            }
        }

        public ModelObjectWithId()
            : base()
        {
            m_id = default;
        }
    }
}
