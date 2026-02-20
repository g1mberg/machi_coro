namespace Game.Models.Dice;

public class DiceResult(int sum, bool isDouble, int die1 = 0, int die2 = 0)
{
    public int Sum { get; init; } = sum;
    public bool IsDouble { get; init; } = isDouble;
    public int Die1 { get; init; } = die1;
    public int Die2 { get; init; } = die2;

    public static byte[] SerializeDice(DiceResult d)
    {
        var bytes = new byte[13]; // 4 (Sum) + 1 (IsDouble) + 4 (Die1) + 4 (Die2)
        Buffer.BlockCopy(BitConverter.GetBytes(d.Sum), 0, bytes, 0, 4);
        bytes[4] = d.IsDouble ? (byte)1 : (byte)0;
        Buffer.BlockCopy(BitConverter.GetBytes(d.Die1), 0, bytes, 5, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(d.Die2), 0, bytes, 9, 4);
        return bytes;
    }

    public static DiceResult DeserializeDice(byte[] bytes)
    {
        int sum = BitConverter.ToInt32(bytes, 0);
        bool isDouble = bytes[4] != 0;
        int die1 = bytes.Length >= 13 ? BitConverter.ToInt32(bytes, 5) : 0;
        int die2 = bytes.Length >= 13 ? BitConverter.ToInt32(bytes, 9) : 0;
        return new DiceResult(sum, isDouble, die1, die2);
    }
}