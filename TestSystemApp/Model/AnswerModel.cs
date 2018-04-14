using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemApp.Model
{
    public class AnswerModel:BaseModel
    {
        #region IdProperty
		private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged("Id");
                }
            }
        }
		#endregion 

        #region NameProperty
		private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
		#endregion 

        #region IsSelectedProperty
		private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }
        }
		#endregion 

        #region IsRightProperty
		private bool _isRight;
        public bool IsRight
        {
            get { return _isRight; }
            set
            {
                if (_isRight != value)
                {
                    _isRight = value;
                    RaisePropertyChanged("IsRight");
                }
            }
        }
		#endregion 
    }
}
