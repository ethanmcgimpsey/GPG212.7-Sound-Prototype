using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QA", menuName = "Question and Answer", order = 1)] //Allows you to create a scriptable object from the main menu (Check Questions&Answer folder and right click in there) 
public class QuestionAndAnswer : ScriptableObject
{
    public string question; //Creates question
    public string answer; //Creates Answer
}
