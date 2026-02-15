namespace ProtocolFramework.Serializator;

using System.Text;

public static class XPacketStringExtensions
{
    public static void SetString(this XPacket p, byte fieldId, string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value ?? "");
        p.SetValueRaw(fieldId, bytes);
    }

    public static string GetString(this XPacket p, byte fieldId)
    {
        var field = p.GetField(fieldId);
        if (field == null || field.Contents == null || field.FieldSize == 0)
            return "";
        return Encoding.UTF8.GetString(field.Contents);
    }
}
