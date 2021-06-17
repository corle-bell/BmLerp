using System;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensionMethods
{
	public static void SetPositionX(this Transform transform, float x)
	{
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
	}

	public static void SetPositionY(this Transform transform, float y)
	{
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}

	public static void SetPositionZ(this Transform transform, float z)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
	}

	public static void SetLocalPositionX(this Transform transform, float x)
	{
		transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
	}

	public static void SetLocalPositionY(this Transform transform, float y)
	{
		transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
	}

	public static void SetLocalPositionZ(this Transform transform, float z)
	{
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
	}
	public static void SetLocalEulerAngleX(this Transform transform, float x)
	{
		transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
	}


	public static void SetLocalEulerAngleY(this Transform transform, float y)
	{
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
	}

	public static void SetLocalEulerAngleZ(this Transform transform, float z)
	{
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
	}

	public static void SetLocalScaleX(this Transform transform, float x)
	{
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
	}

	public static void SetLocalScaleY(this Transform transform, float y)
	{
		transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
	}

	public static void SetLocalScaleZ(this Transform transform, float z)
	{
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
	}

	public static void SetUniformLocalScale(this Transform transform, float uniformScale)
	{
		transform.localScale = Vector3.one * uniformScale;
	}

	public static void CopyPositionRotation(this Transform transform, Transform targetTransform)
	{
		transform.position = targetTransform.position;
		transform.rotation = targetTransform.rotation;
	}

	public static Transform FindParent(this Transform transform, string parentName)
	{
		Transform parent = transform.parent;
		while (parent != null && parent.name != parentName)
		{
			parent = parent.parent;
		}
		return parent;
	}

	public static T FindComponentInParent<T>(this Transform transform) where T : Component
	{
		T t = default(T);
		Transform transform2 = transform;
		do
		{
			t = transform2.GetComponent<T>();
			transform2 = transform2.parent;
		}
		while (t == null && transform2 != null);
		return t;
	}

	public static Transform FindChildDeep(this Transform transform, string childName)
	{
		if (transform.name == childName)
		{
			return transform;
		}
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (child.name == childName)
			{
				return child;
			}
			Transform transform2 = child.FindChildDeep(childName);
			if (transform2 != null)
			{
				return transform2;
			}
		}
		return null;
	}
	public static List<Transform> FindChildrenDeep(this Transform transform, string childName)
	{
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (child.name == childName)
			{
				list.Add(child);
			}
			if (child.childCount > 0)
			{
				list.AddRange(child.FindChildrenDeep(childName));
			}
		}
		return list;
	}

	public static float DistanceTo(this Transform transform, Transform target)
	{
		return Vector3.Distance(transform.position, target.position);
	}

	public static float DistanceTo(this Transform transform, GameObject target)
	{
		return Vector3.Distance(transform.position, target.transform.position);
	}

	public static float DistanceTo(this Transform transform, Component target)
	{
		return Vector3.Distance(transform.position, target.transform.position);
	}

	public static float DistanceTo(this Transform transform, Vector3 position)
	{
		return Vector3.Distance(transform.position, position);
	}

	public static float DistanceTo_XY(this Transform transform, Vector3 position)
	{
		var left = transform.position;
		var right = position;
		left.z = right.z = 0;
		return Vector3.Distance(left, right);
	}

	public static float SqrDistanceTo(this Transform transform, Transform target)
	{
		return Vector3.SqrMagnitude(transform.position - target.position);
	}

	public static float SqrDistanceTo(this Transform transform, GameObject target)
	{
		return Vector3.SqrMagnitude(transform.position - target.transform.position);
	}

	public static float SqrDistanceTo(this Transform transform, Component target)
	{
		return Vector3.SqrMagnitude(transform.position - target.transform.position);
	}

	public static float SqrDistanceTo(this Transform transform, Vector3 position)
	{
		return Vector3.SqrMagnitude(transform.position - position);
	}

	public static Quaternion WorldToLocalRotationInParent(this Transform transform, Quaternion worldRotation)
	{
		if (transform.parent != null)
		{
			return Quaternion.Inverse(transform.parent.rotation) * worldRotation;
		}
		return worldRotation;
	}

	public static string HierarchyName(this Transform transform)
	{
		string text = transform.name;
		while (transform.parent != null)
		{
			text = transform.parent.name + "/" + text;
			transform = transform.parent;
		}
		return text;
	}
}
