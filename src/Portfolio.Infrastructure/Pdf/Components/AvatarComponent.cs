//using QuestPDF.Fluent;
//using QuestPDF.Helpers;
//using QuestPDF.Infrastructure;
//using SkiaSharp;

//namespace Portfolio.Infrastructure.Pdf.Components
//{
//    public class AvatarComponent : IComponent
//    {
//        private readonly byte[]? _imageData;
//        private readonly string _name;

//        public AvatarComponent(byte[]? imageData, string name)
//        {
//            _imageData = imageData;
//            _name = name;
//        }

//        public void Compose(IContainer container)
//        {
//            container.AlignCenter().Height(80).Width(80).Layers(layers =>
//            {
//                layers.Layer().Image(GenerateCircleStream());

//                layers.PrimaryLayer().AlignCenter().AlignMiddle().Column(col =>
//                {
//                    if (_imageData != null && _imageData.Length > 0)
//                    {
//                        col.Item().Image(_imageData, ImageScaling.FitArea);
//                    }
//                    else
//                    {
//                        col.Item().PaddingBottom(2).Text(_name.Substring(0, 1))
//                                  .FontSize(30).Bold().FontColor(Colors.White);
//                    }
//                });
//            });
//        }

//        private byte[] GenerateCircleStream()
//        {
//            using var surface = SKSurface.Create(new SKImageInfo(200, 200));
//            var canvas = surface.Canvas;
//            canvas.Clear(SKColors.Transparent);

//            using var paint = new SKPaint
//            {
//                Color = SKColor.Parse(CvStyles.PrimaryColorHex),
//                IsAntialias = true
//            };

//            canvas.DrawCircle(100, 100, 100, paint);

//            using var image = surface.Snapshot();
//            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
//            return data.ToArray();
//        }
//    }
//}
