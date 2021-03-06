﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Threading.Tasks;
using Assets.Scripts.Web;
using Assets.Scripts.Util;
using Assets.Scripts.GameMain;

namespace Tests
{
    public class StompBasic
    {
        // A Test behaves as an ordinary method
        [Test]
        public void StompBasicSimplePasses()
        {
			List<Tile> tiles = new List<Tile>()
			{
				new Tile(0x00000),
				new Tile(0xf0010),
				new Tile(0xf0021),
				new Tile(0xf0022),
				new Tile(0xf0030),
				new Tile(0xf0031),
				//new Tile(0xf0032),
				new Tile(0xf0042),
				//new Tile(0xf0043),
				//new Tile(0xf0050),
				new Tile(0xf0053),
			};
			Debug.Log(Rule.BasicCanWin(tiles));
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator StompBasicWithEnumeratorPasses()
        {
            
            yield return null;
        }
    }
}
