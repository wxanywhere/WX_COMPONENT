using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelBase;

namespace ISOAClient
{
    public interface IPackingProvider
    {
        string OrganizeInput(string serviceName,params object[] inParams);

        Tuple<T1, T2> OrganizeOutput<T1, T2>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new();

        Tuple<T1, T2, T3[]> OrganizeOutput<T1, T2, T3>(byte[] reactBytes) 
            where T1:VOBase,new ()
            where T2 : VOBase, new()
            where T3 : VOBase, new();

        Tuple<T1, T2, T3[],T4[]> OrganizeOutput<T1, T2, T3,T4>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new();

        Tuple<T1, T2, T3[], T4[], T5[]> OrganizeOutput<T1, T2, T3, T4, T5>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new();

        Tuple<T1, T2, T3[], T4[], T5[], T6[]> OrganizeOutput<T1, T2, T3, T4, T5, T6>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new()
            where T6 : VOBase, new();

        Tuple<T1, T2, T3[], T4[], T5[], T6[],T7[]> OrganizeOutput<T1, T2, T3, T4, T5, T6, T7>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new()
            where T6 : VOBase, new()
            where T7 : VOBase, new();
    }
}
