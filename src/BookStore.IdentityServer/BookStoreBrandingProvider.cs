﻿using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace BookStore
{
    [Dependency(ReplaceServices = true)]
    public class BookStoreBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "BookStore";
    }
}
