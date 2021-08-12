using Emp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emp.Models
{
    public class DeptRepository : IDeptRepository
    {
        private readonly EmployeeTableContext _employeeTableContext;
        public DeptRepository(EmployeeTableContext employeeTableContext)
        {
            _employeeTableContext = employeeTableContext;
        }

        public DeptTable Add(DeptTable dept)
        {
            _employeeTableContext.DeptTable.Add(dept);
            _employeeTableContext.SaveChanges();
            return dept;
        }

        public DeptTable Find(int Id)
        {
            var deptrecord = _employeeTableContext.DeptTable.Find(Id);
            return deptrecord;
        }

        public List<DeptTable> GetAll()
        {
            return _employeeTableContext.DeptTable.Where(d=>d.IsActive==true).ToList();
        }

        public DeptTable DeleteDepartment(int Id)
        {
            try
            {
                var deptEntity = Find(Id);
                deptEntity.IsActive = false;
                _employeeTableContext.Emptable.Join(_employeeTableContext.DeptTable, emp => emp.DeptId, dep => dep.DeptId, (emp,dep) => new { emp,dep })
                    .Where(x => x.emp.DeptId == Id)
                    .Select(x => x.dep).UpdateFromQueryAsync(x => new Emptable { IsActive = false });
                _employeeTableContext.SaveChanges();
                return deptEntity;
            }
            catch
            {

            }
         }

        public DeptTable Update(DeptTable dept)
        {
            var deptEntity = Find(dept.DeptId);
            deptEntity.DeptName = dept.DeptName;
            _employeeTableContext.SaveChanges();
            return deptEntity;
        }
        public List<DeptTable> GetInactiveDepartments()
        {

             return _employeeTableContext.DeptTable.Where(d => d.IsActive == false).ToList();
            //return _employeeTableContext.Emptable.ToList();
        }
    }
}
