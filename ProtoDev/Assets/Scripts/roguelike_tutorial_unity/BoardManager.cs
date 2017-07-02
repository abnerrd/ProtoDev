using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int _Columns = 8;
    public int _Rows    = 8;

    private List<Vector3> m_gridPositions = new List<Vector3>();

    void InitializeList()
    {
        m_gridPositions.Clear();

        for(int x = 1; x < _Columns - 1; ++x)
        {
            for(int y = 1; y < _Rows - 1; ++y)
            {
                m_gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {

    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
