using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Exceptions;
using Newtonsoft.Json;

namespace CUE4Parse.UE4.Objects.UObject;

[JsonConverter(typeof(FFormatContainerConverter))]
public class FFormatContainer
{
    public readonly SortedDictionary<FName, FByteBulkData> Formats;

    public FFormatContainer(FAssetArchive Ar)
    {
        var numFormats = Ar.Read<int>();
        if (numFormats < 0) throw new ParserException(Ar, $"Invalid cooked format count: {numFormats}");
        Formats = new SortedDictionary<FName, FByteBulkData>();
        for (var i = 0; i < numFormats; i++)
        {
            Formats[Ar.ReadFName()] = new FByteBulkData(Ar);
        }
    }
}