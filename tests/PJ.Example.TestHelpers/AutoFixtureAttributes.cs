﻿using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace PJ.Example.TestHelpers
{
    //[ExcludeFromCodeCoverage]
    public sealed class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }

    //[ExcludeFromCodeCoverage]
    public sealed class InlineAutoNSubstituteDataAttribute : CompositeDataAttribute
    {
        public InlineAutoNSubstituteDataAttribute([NotNull] params object[] values)
            : base(new InlineDataAttribute(values),
                   new AutoNSubstituteDataAttribute())
        {
        }
    }
}