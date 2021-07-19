using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
namespace Bm.Lerp
{
    public class BmLerpProperty_Float : BmLerpBase
    {
        [HideInInspector]
        public Component component;
        [HideInInspector]
        public string Property;

        private PropertyInfo _propertyInfo;

        public override void Init()
        {
            base.Init();
            if(_propertyInfo==null) _propertyInfo = component.GetType().GetProperty(Property);
        }

        protected override void _Lerp(float _per)
        {          
            if (_propertyInfo!=null)
            {
                _propertyInfo.SetValue(component, _per);
            }
        }
    }
}

