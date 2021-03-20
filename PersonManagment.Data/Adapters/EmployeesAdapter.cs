using CamundaClient.Dto;
using CamundaClient.Worker;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonManagment.Data.Adapters
{
    [ExternalTaskTopic("GetEmployees")]
    class EmployeesAdapter: IExternalTaskAdapter
    {
        public void Execute(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            //Console.WriteLine();
            //Console.WriteLine("Getting employees!");
            //Console.WriteLine();

        }
    }
}
