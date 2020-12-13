using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrolView : MonoBehaviour
{
    public RectTransform prefab;
    public int modelsCount = 10;
    public RectTransform content;
    private int position = 1;

    public void StartU()
    {
        WWW www = new WWW("http://localhost/my_php/DestroyerKnight/recordAll.php");
        StartCoroutine(GetItem(www, results => OnReceivedModels(results)));
    }
    void OnReceivedModels(TestItemModel[] models)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        foreach (var model in models)
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
            instance.transform.SetParent(content, false);
            InitializeItemView(instance, model);
            position++;
        }
    }
    void InitializeItemView(GameObject viewGameObject, TestItemModel model)
    {
        TestItemView view = new TestItemView(viewGameObject.transform);
        view.pos.text = position.ToString();
        view.name.text = model.name;
        view.time.text = model.time;
        view.store.text = model.store;
    }
    //получение json строки 
    IEnumerator GetItem(WWW www, System.Action<TestItemModel[]> callback)
    {
        yield return www;

        if (www.error == null)
        {
            TestItemModel[] mList = JsonHelper.getJsonArray<TestItemModel>(www.text);
            Debug.Log("server onswer: " + www.text);
            callback(mList);
        }
        else
        {
            Debug.Log("Error: " + www.error);
        }
    }

    public class TestItemView
    {
        public Text pos;
        public Text name;
        public Text time;
        public Text store;
        public TestItemView(Transform rootView)
        {
            pos = rootView.Find("Pos").GetComponent<Text>();
            name = rootView.Find("Name").GetComponent<Text>();
            time = rootView.Find("Time").GetComponent<Text>();
            store = rootView.Find("Store").GetComponent<Text>();
        }
    }
    //парсер
    [System.Serializable]
    public class TestItemModel
    {
        public string name;
        public string time;
        public string store;
    }
    public class JsonHelper
    {
        public static T[] getJsonArray<T>(string json)
        {
            string newJson = "{ \"array\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        public static string arrayToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.array = array;
            return JsonUtility.ToJson(wrapper);
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] array;
        }
    }
}
