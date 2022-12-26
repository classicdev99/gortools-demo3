// See https://aka.ms/new-console-template for more information

using PM.Scheduler.GTools;
using PM.Scheduler.GTools.Models;

Console.WriteLine("Hello, World!");
//RunDemo1();
//RunDemo2();
RunDemo3();
RunDemo4();
/*
 * Workshop with three machines. 
 * We have to schedule two operations, with two process each operation.
 * Op1: process1 can be done in Fab01 (takes 10 hours) or Fab02 (takes 20 hours to complete). Process 2 have to be done in Emb01
 * Op2: process1 can be done in Fab01 (takes 15 hours) or Fab02 (takes 12 hours to complete). Process 2 have to be done in Emb01
 * Additional consideration: Subproducts of process1 cannot be waiting more than 1 hour to be completed in process2 (MaxWaitTime=1)
 * 
 * Solution: 
 *  Resource Fab01:: | op1_1  |
 *  Resource Emb01:: **********|op1_2|p2_2|
 *  Resource Fab02:: **|  op2_1   |
 *  
 *  Fab1 starts producing 1.1, when it finished starts 1.2 in Emb01
 *  Fab2 delay to start with 2.1, because if starts in time 0, when it finish there won't be available machine to make process 2.2
 *  
 */
void RunDemo1()
{

    //1. Definimos recursos
    Resource fabUno = new Resource("Fab01");
    Resource fabDos = new Resource("Fab02");
    Resource embotelladora = new Resource("Emb01");

    //2. Definimos operaciones con sus procesos
    //2.1 Op1
    Process op1_1 = new Process("op1_1", "Fabricación");
    op1_1.AddAllowedResource(fabUno, 10);
    op1_1.AddAllowedResource(fabDos, 20);
    op1_1.MaxWaitTime = 1;
    Process op1_2 = new Process("op1_2", "Embotellado");
    op1_2.AddAllowedResource(embotelladora, 5);

    Operation op1 = new("Op01", "Operación 1");
    op1.AddProcess(op1_1, op1_2);

    //2.2 Op2
    Process op2_1 = new Process("op2_1", "Fabricación");
    op2_1.AddAllowedResource(fabUno, 15);
    op2_1.AddAllowedResource(fabDos, 12);
    op2_1.MaxWaitTime = 1;

    Process op2_2 = new Process("op2_2", "Embotellado");
    op2_2.AddAllowedResource(embotelladora, 5);

    Operation op2 = new("Op02", "Operación 2");
    op2.AddProcess(op2_1, op2_2);

    //3. Calculos
    Engine engine = new();
    engine.AddOperations(op1, op2);

    engine.Calculate();

    foreach (var item in engine.Scheduling.Keys)
    {
        Utils.Print(item.Code, engine.Scheduling[item]);
    }
}



/*
 * Demo 2 is a simpler workshop. There are 4 single process operations. 
 * There are two resources (machines), every process can be done in resource Fab01 or Fab02, with different times to finish
 * Solution:
 *  Resource Fab01:: | op4_1  || op3_1  || op1_1  |
 *  Resource Fab02:: |     op2_1      |
 *  
 *  op 2 is done in fab02, anothers operation choose faster resource Fab01
 */
void RunDemo2()
{

    //1. Definimos recursos
    Resource fabUno = new Resource("Fab01");
    Resource fabDos = new Resource("Fab02");

    //2. Definimos operaciones con sus procesos
    //2.1 Op1
    Process op1_1 = new Process("op1_1", "Fabricación");
    op1_1.AddAllowedResource(fabUno, 10);
    op1_1.AddAllowedResource(fabDos, 16);
    op1_1.MaxWaitTime = 1;
    Operation op1 = new("Op01", "Operación 1");
    op1.AddProcess(op1_1);

    //2.1 Op1
    Process op2_1 = new Process("op2_1", "Fabricación");
    op2_1.AddAllowedResource(fabUno, 15);
    op2_1.AddAllowedResource(fabDos, 18);
    op2_1.MaxWaitTime = 1;
    Operation op2 = new("Op02", "Operación 2");
    op2.AddProcess(op2_1);

    //2.1 Op1
    Process op3_1 = new Process("op3_1", "Fabricación");
    op3_1.AddAllowedResource(fabUno, 10);
    op3_1.AddAllowedResource(fabDos, 16);
    op3_1.MaxWaitTime = 1;
    Operation op3 = new("Op03", "Operación 3");
    op3.AddProcess(op3_1);

    //2.1 Op1
    Process op4_1 = new Process("op4_1", "Fabricación");
    op4_1.AddAllowedResource(fabUno, 10);
    op4_1.AddAllowedResource(fabDos, 16);
    op4_1.MaxWaitTime = 1;
    Operation op4 = new("Op04", "Operación 4");
    op4.AddProcess(op4_1);

    //3. Calculos
    Engine engine = new();
    engine.AddOperations(op1, op2, op3, op4);

    engine.Calculate();

    foreach (var item in engine.Scheduling.Keys)
    {
        Utils.Print(item.Code, engine.Scheduling[item]);
    }
}

/*
 * Demo 3 is the problem to be solved
 * Is similar to Demo2, but now, we have different ProcessTypes
 * Each time a machine change to a new process, it has a "setup time" to be prepared to the new process
 * Setup time depends of ProcessType previous and new
 */
void RunDemo3()
{
    //TODO: Introduce setup time from this array
    List<List<int>> setupArray = new List<List<int>>();
    setupArray.Add(new List<int>() {0,2 }); //(prev=0, next=0)=>setup=0h; (prev=0, next=1)=>setup=2h; 
    setupArray.Add(new List<int>() {1,0 }); //(prev=1, next=0)=>setup=1h; (prev=1, next=1)=>setup=0h; 

    //1. Definimos recursos
    Resource fabUno = new Resource("Fab01");
    Resource fabDos = new Resource("Fab02");

    //2. Definimos operaciones con sus procesos
    //2.1 Op1
    Process op1_1 = new Process("op1_1", "Fabricación");
    op1_1.AddAllowedResource(fabUno, 10);
    //op1_1.AddAllowedResource(fabDos, 16);
    op1_1.MaxWaitTime = 1;
    op1_1.ProcessType = 1;
    Operation op1 = new("Op01", "Operación 1");
    op1.AddProcess(op1_1);

    //2.1 Op1
    Process op2_1 = new Process("op2_1", "Fabricación");
    op2_1.AddAllowedResource(fabUno, 15);
    op2_1.AddAllowedResource(fabDos, 18);
    op2_1.MaxWaitTime = 1;
    op2_1.ProcessType = 0;
    Operation op2 = new("Op02", "Operación 2");
    op2.AddProcess(op2_1);

    //2.1 Op1
    Process op3_1 = new Process("op3_1", "Fabricación");
    op3_1.AddAllowedResource(fabUno, 10);
    op3_1.AddAllowedResource(fabDos, 16);
    op3_1.MaxWaitTime = 1;
    op3_1.ProcessType = 0;
    Operation op3 = new("Op03", "Operación 3");
    op3.AddProcess(op3_1);

    //2.1 Op1
    Process op4_1 = new Process("op4_1", "Fabricación");
    op4_1.AddAllowedResource(fabUno, 10);
    op4_1.AddAllowedResource(fabDos, 16);
    op4_1.MaxWaitTime = 1;
    op4_1.ProcessType = 1;
    Operation op4 = new("Op04", "Operación 4");
    op4.AddProcess(op4_1);

    //3. Calculos
    Engine engine = new();
    engine.AddOperations(op1, op2, op3, op4);

    engine.Calculate(setupArray);

    foreach (var item in engine.Scheduling.Keys)
    {
        Utils.Print(item.Code, engine.Scheduling[item]);
    }
}

void RunDemo4()
{
    //TODO: Introduce setup time from this array
    List<List<int>> setupArray = new List<List<int>>();
    setupArray.Add(new List<int>() { 0, 2 ,1, 2}); 
    setupArray.Add(new List<int>() { 1, 0 ,3, 3}); 
    setupArray.Add(new List<int>() { 1, 4, 1, 3});
    setupArray.Add(new List<int>() { 1, 8, 3, 0});

    //1. Definimos recursos
    Resource fabUno = new Resource("Fab01");
    Resource fabDos = new Resource("Fab02");

    //2. Definimos operaciones con sus procesos
    //2.1 Op1
    Process op1_1 = new Process("op1_1", "Fabricación");
    op1_1.AddAllowedResource(fabUno, 12);
    op1_1.AddAllowedResource(fabDos, 13);
    op1_1.MaxWaitTime = 1;
    op1_1.ProcessType = 2;
    Operation op1 = new("Op01", "Operación 1");
    op1.AddProcess(op1_1);

    //2.1 Op1
    Process op2_1 = new Process("op2_1", "Fabricación");
    op2_1.AddAllowedResource(fabUno, 15);
    op2_1.AddAllowedResource(fabDos, 13);
    op2_1.MaxWaitTime = 1;
    op2_1.ProcessType = 1;
    Operation op2 = new("Op02", "Operación 2");
    op2.AddProcess(op2_1);

    //2.1 Op1
    Process op3_1 = new Process("op3_1", "Fabricación");
    op3_1.AddAllowedResource(fabUno, 16);
    op3_1.AddAllowedResource(fabDos, 13);
    op3_1.MaxWaitTime = 1;
    op3_1.ProcessType = 0;
    Operation op3 = new("Op03", "Operación 3");
    op3.AddProcess(op3_1);

    //2.1 Op1
    Process op4_1 = new Process("op4_1", "Fabricación");
    op4_1.AddAllowedResource(fabUno, 12);
    op4_1.AddAllowedResource(fabDos, 16);
    op4_1.MaxWaitTime = 1;
    op4_1.ProcessType = 3;
    Operation op4 = new("Op04", "Operación 4");
    op4.AddProcess(op4_1);

    Process op5_1 = new Process("op5_1", "Fabricación");
    op5_1.AddAllowedResource(fabUno, 18);
    op5_1.AddAllowedResource(fabDos, 19);
    op5_1.MaxWaitTime = 1;
    op5_1.ProcessType = 2;
    Operation op5 = new("Op05", "Operación 5");
    op5.AddProcess(op5_1);

    //3. Calculos
    Engine engine = new();
    engine.AddOperations(op1, op2, op3, op4, op5);

    engine.Calculate(setupArray);

    foreach (var item in engine.Scheduling.Keys)
    {
        Utils.Print(item.Code, engine.Scheduling[item]);
    }
}