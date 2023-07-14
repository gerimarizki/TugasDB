using MVCArchitecture.Models;
using MVCArchitecture.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Controllers
{
    public class EmployeeController
    {
        private Employee _employeeModel;
        private VEmployee _employeeView;

        public EmployeeController(Employee historyModel, VEmployee historyView)
        {
            _employeeModel = historyModel;
            _employeeView = historyView;
        }

        public void GetAll()
        {
            var employeeResult = _employeeModel.GetAll();
            if (employeeResult.Count() is 0)
            {
                _employeeView.DataEmpty();
            }
            else
            {
                _employeeView.GetAll(employeeResult);
            }
        }

        public void Insert()
        {
            var employee = _employeeView.InsertMenu();

            var result = _employeeModel.Insert(employee);
            switch (result)
            {
                case -1:
                    _employeeView.DataEmpty();
                    break;
                case 0:
                    _employeeView.Fail();
                    break;
                default:
                    _employeeView.Success();
                    break;
            }
        }

        public void Update()
        {
            var employee = _employeeView.UpdateMenu();
            var result = _employeeModel.Update(employee);

            switch (result)
            {
                case -1:
                    _employeeView.DataEmpty();
                    break;
                case 0:
                    _employeeView.Fail();
                    break;
                default:
                    _employeeView.Success();
                    break;
            }
        }

        public void Delete()
        {
            var employee = _employeeView.DeleteMenu();
            var result = _employeeModel.Delete(employee);

            switch (result)
            {
                case -1:
                    _employeeView.DataEmpty();
                    break;
                case 0:
                    _employeeView.Fail();
                    break;
                default:
                    _employeeView.Success();
                    break;
            }

        }

        public void SearchById()
        {
            int id = _employeeView.GetEmployeeId();
            var result = _employeeModel.GetById(id);
            if (result == null)
            {
                _employeeView.DataEmpty();
            }
            else
            {
                _employeeView.GetById(result);
            }
        }
    }
}
