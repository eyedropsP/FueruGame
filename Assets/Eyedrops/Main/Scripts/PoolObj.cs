using System.Collections.Generic;
using UnityEngine;

namespace Eyedrops.Main.Scripts
{
	public abstract class PoolObj<T> : MonoBehaviour
	{
		private static GameObject mOriginal;
		private static Stack<T> mObjPool = new Stack<T>();
		
		public static void SetOriginal(GameObject origin)
		{
			mOriginal = origin;
		}

		public static T Create()
		{
			T obj;
			if (mObjPool.Count > 0)
			{
				obj = Pop();
			}
			else
			{
				var go = Instantiate(mOriginal);
				obj = go.GetComponent<T>();
			}

			(obj as PoolObj<T>)?.Init();
			return obj;
		}

		public static T Create(GameObject parent)
		{
			T obj;
			if (mObjPool.Count > 0)
			{
				obj = Pop();
			}
			else
			{
				var go = Instantiate(mOriginal, parent.transform);
				obj = go.GetComponent<T>();
			}
			
			(obj as PoolObj<T>)?.Init();
			return obj;
		}

		private static T Pop()
		{
			var ret = mObjPool.Pop();
			return ret;
		}
		
		protected static void Pool(T obj)
		{
			(obj as PoolObj<T>)?.Sleep();
			mObjPool.Push(obj);
		}
		
		public static void Clear()
		{
			mObjPool.Clear();
		}
		
		protected abstract void Init();
		protected abstract void Sleep();
	}
}