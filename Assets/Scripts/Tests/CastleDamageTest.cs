using ArcomageClone.Units;
using NUnit.Framework;
using System;
using UnityEngine;

namespace ArcomageClone.Tests
{
    public class CastleDamageTest
    {
        bool debug = false;
        Player player;
        readonly int wallHealth = 10;
        readonly int castleHealth = 25;
        readonly int lowerDamage = 3;
        readonly int higherDamage = 20;

        public CastleDamageTest()
        {
            if (debug) Debug.Log("Testing damage...");
            var go = GameObject.Instantiate(new GameObject());
            var castle = GameObject.Instantiate(new GameObject());
            var wall = GameObject.Instantiate(new GameObject());
            go.name = "player";
            castle.name = "castle";
            wall.name = "wall";
            player = go.AddComponent<Player>();
            player.Castle = castle.AddComponent<Castle>();
            player.Wall = wall.AddComponent<Wall>();
        }

        [SetUp]
        public void Setup()
        {
            if (debug) Debug.Log($"Setting up. C: {castleHealth} | W: {wallHealth}");
            player.Wall.Health = wallHealth;
            player.Castle.Health = castleHealth;
        }

        [Test]
        public void AmountLower()
        {
            if (debug) Debug.Log("Testing lower damage");
            player.Damage(lowerDamage);
            Assert.AreEqual(player.Wall.Health, wallHealth - lowerDamage);
            Assert.AreEqual(player.Castle.Health, castleHealth);
            if (debug) Debug.Log($"C: {player.Castle.Health} | W: {player.Wall.Health} | Dmg: {lowerDamage}");
        }

        [Test]
        public void AmountEqual()
        {
            if (debug) Debug.Log("Testing equal damage");
            player.Damage(wallHealth);
            Assert.AreEqual(player.Wall.Health, 0);
            Assert.AreEqual(player.Castle.Health, castleHealth);
            if (debug) Debug.Log($"C: {player.Castle.Health} | W: {player.Wall.Health} | Dmg: {wallHealth}");
        }

        [Test]
        public void AmountHigher()
        {
            if (debug) Debug.Log("Testing high damage");
            player.Damage(higherDamage);
            Assert.AreEqual(player.Wall.Health, 0);
            Assert.AreEqual(player.Castle.Health, castleHealth - (higherDamage - wallHealth));
            if (debug) Debug.Log($"C: {player.Castle.Health} | W: {player.Wall.Health} | Dmg: {higherDamage}");
        }
    }
}