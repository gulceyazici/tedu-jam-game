using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {

        }
    }
}
