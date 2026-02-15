namespace Game.Models.Dice;

public class DiceResult(int sum, bool isDouble)
{
    public int Sum {  get; init; } = sum;
    public bool IsDouble { get; init; } = isDouble;

    public static byte[]  SerializeDice(DiceResult d)
    {
        var bytes = new byte[5]; // 4 байта int + 1 байт bool
        Buffer.BlockCopy(BitConverter.GetBytes(d.Sum), 0, bytes, 0, 4);
        bytes[4] = d.IsDouble ? (byte)1 : (byte)0;
        return bytes;
    }

    public static DiceResult DeserializeDice(byte[] bytes)
    {
        int sum = BitConverter.ToInt32(bytes, 0);
        bool isDouble = bytes[4] != 0;
        return new DiceResult(sum, isDouble);
    }
}