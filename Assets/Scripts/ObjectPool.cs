using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component {
    private List<T> pool = new List<T>();
    private int count;

    public ObjectPool(T gameObject, int initialCount) {
        for (int i = 0; i < initialCount; i++) {
            CreateObject(gameObject);
        }
    }

    public T GetObject() {
        if (count > 0) {
            T obj = pool[--count];
            obj.gameObject.SetActive(true);
            return obj;
        }
        else {
            Debug.LogWarning("Object pool is empty!");
            return null;
        }
    }

    public void Recycle(T obj) {
        obj.gameObject.SetActive(false);
        pool[count++] = obj;
    }

    private void CreateObject(T prefab) {
        T obj = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.gameObject.SetActive(false);
        pool.Add(obj);
        count++;
    }
}