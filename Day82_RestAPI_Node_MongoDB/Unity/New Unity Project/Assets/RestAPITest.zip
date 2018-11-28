using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestAPITest : MonoBehaviour {

    string baseURL = "http://localhost:5334";

    IEnumerator Start()
    {
        yield return AddUser("yy@yy", "userNameyyy", "nickNameyyy");
        yield return GetUser("yy@yy");
        yield return UpdateUser("yy@yy", "Updatedyyy", "Updatedyyy");
        yield return RemoveUser("yy@yy");
    }

    IEnumerator RemoveUser(string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);

        WWW www = new WWW(baseURL + "/user/remove", form);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
            print("Request error : " + www.error);
        else
            print(www.text);
    }

    IEnumerator UpdateUser(string email, string userName, string nickName)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("userName", userName);
        form.AddField("nickName", nickName);

        WWW www = new WWW(baseURL + "/user/update", form);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
            print("Request error : " + www.error);
        else
            print(www.text);
    }

    IEnumerator GetUser(string email)
    {
        WWW www = new WWW(baseURL + "/user/" + email);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
            print("Request error : " + www.error);
        else
            print(www.text);
    }

    IEnumerator AddUser(string email, string userName, string nickName)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("userName", userName);
        form.AddField("nickName", nickName);

        WWW www = new WWW(baseURL + "/user/add", form);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
            print("Request error : " + www.error);
        else
            print(www.text);
    }
}
