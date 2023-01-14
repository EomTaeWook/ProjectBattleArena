using Kosher.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Internal
{
    //public class Vector<T> : ICollection, IReadOnlyCollection<T>
    //{
    //    internal static T[] EmptyArray = new T[0];
    //    internal T[] _container;
    //    internal long _writeIndex = 0;
    //    internal long _readIndex = 0;
    //    public long Capacity
    //    {
    //        get => _container.LongLength;
    //    }
    //    public bool CanRead { get => _readIndex < _writeIndex; }
    //    public long LongCount { get => _writeIndex - _readIndex; }

    //    public virtual bool IsSynchronized { get => false; }

    //    public object SyncRoot { get => this; }

    //    public int Count { get => (int)(_writeIndex - _readIndex); }

    //    public Vector() : this(0)
    //    {
    //    }
    //    public Vector(int capacity)
    //    {
    //        if (capacity == 0)
    //        {
    //            _container = EmptyArray;
    //        }
    //        else
    //        {
    //            _container = new T[capacity];
    //        }
    //    }
    //    public Vector(long capacity)
    //    {
    //        if (capacity == 0)
    //        {
    //            _container = EmptyArray;
    //        }
    //        else
    //        {
    //            _container = new T[capacity];
    //        }
    //    }

    //    public bool Contains(T item)
    //    {
    //        return Array.IndexOf(_container, item, (int)_readIndex) > 0;
    //    }

    //    public void Add(T item)
    //    {
    //        if (_container.LongLength == _writeIndex)
    //        {
    //            Alloc(_writeIndex + 1);
    //        }
    //        _container[_writeIndex++] = item;
    //    }
    //    public void AddRange(ICollection<T> items)
    //    {
    //        if (_container.LongLength < _writeIndex + items.Count)
    //        {
    //            Alloc(_writeIndex + items.Count);
    //        }
    //        foreach (var item in items)
    //        {
    //            _container[_writeIndex++] = item;
    //        }
    //    }
    //    public void AddRange(T[] items)
    //    {
    //        if (_container.LongLength < _writeIndex + items.Length)
    //        {
    //            Alloc(_writeIndex + items.Length);
    //        }
    //        foreach (var item in items)
    //        {
    //            _container[_writeIndex++] = item;
    //        }
    //    }
    //    public T Peek()
    //    {
    //        return _container[_readIndex];
    //    }
    //    public T[] Peek(int size)
    //    {
    //        long lSize = size;
    //        return Peek(lSize);
    //    }
    //    public T[] Peek(long size)
    //    {
    //        if (_readIndex + size > _writeIndex)
    //        {
    //            throw new ArgumentOutOfRangeException(nameof(size));
    //        }
    //        var items = new T[size];
    //        for (int i = 0; i < size; ++i)
    //        {
    //            items[i] = _container[_readIndex + i];
    //        }
    //        return items;
    //    }
    //    public T Read()
    //    {
    //        if (CanRead == false)
    //        {
    //            throw new ArgumentOutOfRangeException();
    //        }

    //        var item = _container[_readIndex++];
    //        return item;
    //    }
    //    public T[] Read(int size)
    //    {
    //        long lSize = size;
    //        return Read(lSize);
    //    }
    //    public T[] Read(long size)
    //    {
    //        if (_readIndex + size > _writeIndex)
    //        {
    //            throw new ArgumentOutOfRangeException(nameof(size));
    //        }
    //        var items = new T[size];
    //        for (int i = 0; i < size; ++i)
    //        {
    //            items[i] = _container[_readIndex++];
    //        }
    //        return items;
    //    }
    //    public void Clear()
    //    {
    //        _writeIndex = 0;
    //        _readIndex = 0;
    //    }
    //    private void Alloc(long size)
    //    {
    //        var capacity = size * 2;
    //        if (size > capacity)
    //        {
    //            capacity = size;
    //        }
    //        var newContainer = new T[capacity];

    //        Array.Copy(_container, 0, newContainer, 0, _writeIndex);

    //        _container = newContainer;
    //    }
    //    public void CopyTo(Array array, int index)
    //    {
    //        if (array == null)
    //        {
    //            throw new ArgumentNullException(nameof(array));
    //        }
    //        if (index < 0)
    //        {
    //            throw new ArgumentOutOfRangeException(nameof(index));
    //        }
    //        Array.Copy(_container, _readIndex, array, index, Count);
    //    }
    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }

    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        for (long i = _readIndex; i < _writeIndex; ++i)
    //        {
    //            yield return _container[i];
    //        }
    //    }
    //    public T this[long index]
    //    {
    //        get
    //        {
    //            return _container[index];
    //        }
    //        set
    //        {
    //            _container[index] = value;
    //            if (index > _writeIndex)
    //            {
    //                _writeIndex = index;
    //            }
    //        }
    //    }
    //    public T this[int index]
    //    {
    //        get
    //        {
    //            return _container[index];
    //        }
    //        set
    //        {
    //            _container[index] = value;
    //            if (index > _writeIndex)
    //            {
    //                _writeIndex = index;
    //            }
    //        }
    //    }
    //}
    //public abstract class BaseObjectPool1<T>
    //{
    //    private readonly Vector<T> _itemContainer = new Vector<T>();
    //    private readonly Kosher.Collections.HashSet<T> _activeObjects = new Kosher.Collections.HashSet<T>();

    //    public abstract T CreateItem();
    //    public abstract void Remove(T item);

    //    public T Pop()
    //    {
    //        T item;
    //        if (_itemContainer.LongCount > 0)
    //        {
    //            item = _itemContainer.Read();
    //        }
    //        else
    //        {
    //            item = CreateItem();
    //        }
    //        _activeObjects.Add(item);
    //        return item;
    //    }
    //    public void Push(T item)
    //    {
    //        if (_activeObjects.Contains(item) == true)
    //        {
    //            _activeObjects.Remove(item);
    //        }
    //        if (_itemContainer.Contains(item) == true)
    //        {
    //            return;
    //        }
    //        _itemContainer.Add(item);
    //    }
    //    public void Clear()
    //    {
    //        ClearExceptActive();
    //        foreach (var item in _activeObjects)
    //        {
    //            Remove(item);
    //        }
    //        _activeObjects.Clear();
    //    }
    //    public void ClearExceptActive()
    //    {
    //        while (_itemContainer.LongCount > 0)
    //        {
    //            var item = _itemContainer.Read();
    //            Remove(item);
    //        }
    //        _itemContainer.Clear();
    //    }
    //}
    //public class KosherUnityObjectPool1 : MonoBehaviourSingleton<KosherUnityObjectPool1>
    //{
    //    private class Pool : BaseObjectPool1<GameObject>
    //    {
    //        private readonly GameObject _prefab;

    //        public Pool(GameObject prefab)
    //        {
    //            this._prefab = prefab;
    //        }

    //        public override GameObject CreateItem()
    //        {
    //            var go = GameObject.Instantiate(_prefab);
    //            _poolCacheToMap.Add(go, this);
    //            return go;
    //        }

    //        public T Pop<T>() where T : Component
    //        {
    //            var go = base.Pop();
    //            if (_componentCacheToMap.ContainsKey(go) == false)
    //            {
    //                var component = go.GetComponent<T>();
    //                _componentCacheToMap.Add(go, component);
    //                return component;
    //            }
    //            else
    //            {
    //                return _componentCacheToMap[go] as T;
    //            }
    //        }

    //        public override void Remove(GameObject item)
    //        {
    //            _poolCacheToMap.Remove(item);
    //            _componentCacheToMap.Remove(item);
    //            GameObject.Destroy(item);
    //        }
    //    }

    //    public static T Load<T>(Component component) where T : Component
    //    {
    //        return Load<T>(component.gameObject, null);
    //    }
    //    public static T Load<T>(GameObject prefab) where T : Component
    //    {
    //        return Load<T>(prefab, null);
    //    }

    //    public static T Load<T>(Component component, Transform parent, bool worldPositionStays = false) where T : Component
    //    {
    //        return Load<T>(component.gameObject, parent, worldPositionStays);
    //    }
    //    public static T Load<T>(GameObject prefab, Transform parent, bool worldPositionStays = false) where T : Component
    //    {
    //        var component = KosherUnityObjectPool.Instance.Pop<T>(prefab);
    //        component.gameObject.transform.SetParent(parent, worldPositionStays);
    //        component.gameObject.gameObject.SetActive(true);
    //        return component;
    //    }

    //    private static readonly Dictionary<GameObject, Component> _componentCacheToMap = new Dictionary<GameObject, Component>();

    //    private static readonly Dictionary<GameObject, Pool> _poolCacheToMap = new Dictionary<GameObject, Pool>();

    //    private static readonly Dictionary<GameObject, Pool> _poolsToMap = new Dictionary<GameObject, Pool>();
    //    public GameObject Pop(GameObject item)
    //    {
    //        if (_poolsToMap.ContainsKey(item) == false)
    //        {
    //            var pool = new Pool(item);
    //            _poolsToMap.Add(item, pool);
    //        }
    //        var poolItem = _poolsToMap[item].Pop();
    //        return poolItem;
    //    }
    //    public T Pop<T>(GameObject item) where T : Component
    //    {
    //        if (_poolsToMap.ContainsKey(item) == false)
    //        {
    //            var pool = new Pool(item);
    //            _poolsToMap.Add(item, pool);
    //        }
    //        var poolItem = _poolsToMap[item].Pop<T>();
    //        return poolItem;
    //    }
    //    public T Pop<T>(Component item) where T : Component
    //    {
    //        return Pop<T>(item.gameObject);
    //    }

    //    public void Push(GameObject item)
    //    {
    //        if (_poolCacheToMap.ContainsKey(item) == false)
    //        {
    //            var pool = new Pool(item);
    //            _poolsToMap.Add(item, pool);
    //        }
    //        _poolCacheToMap[item].Push(item);
    //    }
    //    public void Push(Component item)
    //    {
    //        Push(item.gameObject);
    //    }
    //    public void DestroyPool(GameObject prefab)
    //    {
    //        if (_poolCacheToMap.TryGetValue(prefab, out Pool pool) == true)
    //        {
    //            pool.Clear();
    //            _poolCacheToMap.Remove(prefab);
    //        }
    //    }
    //    public void Clear()
    //    {
    //        foreach (var kv in _poolCacheToMap)
    //        {
    //            var pool = kv.Value;
    //            pool.Clear();
    //        }
    //    }
    //    public void ClearExceptActive()
    //    {
    //        foreach (var kv in _poolCacheToMap)
    //        {
    //            var pool = kv.Value;
    //            pool.ClearExceptActive();
    //        }
    //    }
    //}
}
