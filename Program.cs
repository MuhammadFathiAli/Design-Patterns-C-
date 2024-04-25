namespace CommandDP;

internal class Program
{
    static void Main(string[] args)
    {
        /*
         * definition:
            Command Pattern : encapsulates a request as an object...
            therby, letting you parametrize other objects with different request (make the request as paramter passed to other objects)
            queue or log requests and support undoable operations...
         * explanation: 
            - command pattern encapsulates the request by binding together set of acctions on specific reciever (executer)
                it packages the actions and the real executer of actions (reciever)into an object that exposes just on method => Execute();
                when called, Execute() causes actions to be invoked on the receiver. 
                From outside, no other objects really know what actions get performed on what receiver; 
                they just know what if they call Execute() method, their request will be serviced.

            - u can parametrize an object with a command, in next sample we parametrized the remote controll with lightOnCommand, then lightOffCommand
                the remote controller slot object didn't care what command object it had, as long as it implements the command interface0
         */
        Console.WriteLine("Command Design Pattern!");
        var light = new Light();
        var lightOnCommand = new LightOnCommand(light);
        var lightOffCommand = new LightOffCommand(light);    
        RemoteControll remoteControll = new RemoteControll();

        remoteControll.SetCommand(lightOnCommand);
        remoteControll.SlotPressed();
        remoteControll.SetCommand(lightOffCommand);
        remoteControll.SlotPressed();
    }
}

//invoker 
//  holds a command and at some point asks the command to carry out a request by calling its execute() method
internal class RemoteControll
{
    private  ICommand _slot;

    //takes a command 
    //sets its 
    //executes it

    internal void SetCommand(ICommand command)
    {
        _slot = command;
    }

    internal void SlotPressed()
    {
        _slot.Execute();
    }
}

//interface for all commands, execute method will ask the receiver to perform an action
internal interface ICommand
{
    void Execute();
}
//concrete command class
//defines a binding between an action and a receiver. the invoker makes a request by calling execute()
//and the concretecommand carries it out by calling one or more actions on the recevier.
internal class LightOnCommand : ICommand
{
    private readonly Light _light;
    public LightOnCommand(Light light)
    {
        _light = light; 
    }
    public void Execute()
    {
        // the execute() method invokes the actions on the receiver needed to fulfill the request.
        //receiver.action()
        _light.LightOn();
    }
}
//concrete command class
internal class LightOffCommand : ICommand
{
    private readonly Light _light;
    public LightOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.LightOff();
    }
}

//reciever
internal class Light 
{
    internal void LightOn() => Console.WriteLine("Lights On!!!");
    internal void LightOff() => Console.WriteLine("Lights Off!!!");
}