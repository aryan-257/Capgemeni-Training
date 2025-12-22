using System;

namespace InterfaceXDemoProj
{
    interface IAdd
    {
        int AddMe(int num1, int num2);

    }
    interface ISub
    {
        int SubtMe(int num1, int num2);

    }
    interface IProd
    {
        int ProdtMe(int num1, int num2);

    }
    interface IDiv
    {
        int DivMe(int num1, int num2);

    }
    interface IAddSub : IAdd, ISub
    {
        
    }
    interface IAddProdDiv : IAdd, IProd, IDiv
    {

    }
    interface IAll : IAddProdDiv, ISub
    {

    }

}
