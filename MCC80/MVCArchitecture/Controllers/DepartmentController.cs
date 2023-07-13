using MVCArchitecture.Models;
using MVCArchitecture.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Controllers
{
    public class DepartmentController
    {
        private Department _departmentModel;
        private VDepartment _departmentView;

        public DepartmentController(Department departmentModel, VDepartment departmentView)
        {
            _departmentModel = departmentModel;
            _departmentView = departmentView;
        }

        public void GetAll()
        {
            var regionResult = _departmentModel.GetAll();
            if (regionResult.Count() is 0)
            {
                _departmentView.DataEmpty();
            }
            else
            {
                _departmentView.GetAll(regionResult);
            }
        }

        public void Insert()
        {
            var region = _departmentView.InsertMenu();

            var result = _departmentModel.Insert(region);
            switch (result)
            {
                case -1:
                    _departmentView.DataEmpty();
                    break;
                case 0:
                    _departmentView.Fail();
                    break;
                default:
                    _departmentView.Success();
                    break;
            }
        }

        public void Update()
        {
            var region = _departmentView.UpdateMenu();
            var result = _departmentModel.Update(region);

            switch (result)
            {
                case -1:
                    _departmentView.DataEmpty();
                    break;
                case 0:
                    _departmentView.Fail();
                    break;
                default:
                    _departmentView.Success();
                    break;
            }
        }

        public void Delete()
        {
            var region = _departmentView.DeleteMenu();
            var result = _departmentModel.Delete(region);

            switch (result)
            {
                case -1:
                    _departmentView.DataEmpty();
                    break;
                case 0:
                    _departmentView.Fail();
                    break;
                default:
                    _departmentView.Success();
                    break;
            }

        }

        public void SearchById()
        {
            int id = _departmentView.GetDepartmentId();
            var result = _departmentModel.GetById(id);
            if (result == null)
            {
                _departmentView.DataEmpty();
            }
            else
            {
                _departmentView.GetById(result);
            }
        }
    }
}
