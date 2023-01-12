using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TemplateContainers;

namespace DataContainer.Generated
{
    public partial class GoodsTemplate : BaseTemplate
    {
        public sealed partial class InnerPreAsset
        {
            public DataContainer.AssetType AssetType { get; set; }
            public int ConsumeAssetCount { get; set; }
        }
        public sealed partial class InnerAsset
        {
            public DataContainer.AssetType AssetType { get; set; }
            public int ConsumeAssetCount { get; set; }
        }
        public DataContainer.GoodsCategory GoodsCategory { get; set; }
        public int Count { get; set; }
        public InnerPreAsset PreAsset { get; set; }
        public InnerAsset Asset { get; set; }
    }
}
