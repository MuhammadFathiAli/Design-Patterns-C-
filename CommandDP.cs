using System.Text;

namespace CommandDP;

internal class CommandDP
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
                the remote controller slot object didn't care what command object it had, as long as it implements the command interface
         */
        Console.WriteLine("Command Design Pattern!");
        var livingRoomLight = new Light("Living room");
        var livingRoomLightOnCommand = new LightOnCommand(livingRoomLight);
        var livingRoomLightOffCommand = new LightOffCommand(livingRoomLight);

        var kitchenLight = new Light("Kitchen");
        var kitchenLightOnCommand = new LightOnCommand(kitchenLight);
        var kitchenLightOffCommand = new LightOffCommand(kitchenLight);
        RemoteControll remoteControll = new RemoteControll();

        remoteControll.SetCommand(0, livingRoomLightOnCommand, livingRoomLightOffCommand);
        remoteControll.SetCommand(1, kitchenLightOnCommand, kitchenLightOffCommand);

        Console.WriteLine(remoteControll.ToString());
        
        
        remoteControll.OnSlotPressed(0);
        remoteControll.OffSlotPressed(0);

        remoteControll.OnSlotPressed(1);
        remoteControll.OffSlotPressed(1);
    }
}

//invoker 
//  holds a command and at some point asks the command to carry out a request by calling its execute() method
internal class RemoteControll
{
    private ICommand[] _onCommands;
    private ICommand[] _offCommands;

    //takes a command 
    //sets its 
    //executes it
    public RemoteControll()
    {
        _onCommands = new ICommand[7];
        _offCommands = new ICommand[7];
        for (int i = 0; i < 7; i++)
        {
            _onCommands[i] = new NoCommand();
            _offCommands[i] = new NoCommand();
        }
    }
    internal void SetCommand(int slotNumber, ICommand onCommand, ICommand offCommand)
    {
        _onCommands[slotNumber] = onCommand;
        _offCommands[slotNumber] = offCommand;
    }

    internal void OnSlotPressed(int slotNumber) => _onCommands[slotNumber].Execute();
    internal void OffSlotPressed(int slotNumber) => _offCommands[slotNumber].Execute();

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < 7; i++)
        {
            stringBuilder.AppendFormat(
                "Remote Slot Number {0} : OnCommand [{1}], OffCommand [{2}] ",
                i,
                _onCommands[i].GetType().Name,
                _offCommands[i].GetType().Name);
            stringBuilder.AppendLine();
        }
        return stringBuilder.ToString();
    }
}

//interface for all commands, execute method will ask the receiver to perform an action
internal interface ICommand
{
    void Execute();
}
internal class NoCommand : ICommand
{
    public void Execute() => Console.WriteLine("No Command assigned yet...");
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
    string _room;
    public Light(string room)
    {
        _room = room;   
    }
    internal void LightOn() => Console.WriteLine("{0} Lights On!!!", _room);
    internal void LightOff() => Console.WriteLine("{0} Lights Off!!!", _room);
}