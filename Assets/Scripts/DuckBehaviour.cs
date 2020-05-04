using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

    public class DuckBehaviour : MonoBehaviour
    {
        public float satiation, nourishment, health, energy;
        public NavMeshAgent agent;
        public TMPro.TMP_Text defeatText;
        public GameObject defeatPanel;


        public float Satiation {
            get => satiation;
            set {
                if (value <= 0) 
                {
                    EndGame();
                }
                else if (value <= 100)
                {
                    satiation = value;
                }
            else
            {
                satiation = 100.0f;
            }
        }
        }
        public float Nourishment {
            get => nourishment;
            set {
                    if (value <= 0)
                    {
                        EndGame();
                    }
                    else if (value <= 100)
                    {
                        nourishment = value;
                    }
            else
            {
                nourishment = 100.0f;
            }
        }
            }
        public float Energy {
            get => energy;
            set {
                if (value <= 100)
                {
                    energy = value;
                }
            }
        }
        public float Health {
            get => health;
            set {
            if (value <= 0)
            {
                EndGame();
            }
            else if (value <= 100)
                {
                    health = value;
                }
                else {
                health = 100.0f;
                }
            }
        }

        void Start()
        {
            Satiation = 100;
            Nourishment = 100;
            Health = 100;
            Energy = 100;
        }
    private void OnCollisionEnter(Collision collision)
    {
        print($"I'm colliding with {collision.gameObject}");
        //if (collision.gameObject.CompareTag("Obstacle"))
        //{
        //    print("Obstacled!");
        //    var obstacle = collision.gameObject.GetComponent<ObstacleBehaviour>();
        //    this.Health += obstacle.HealthDamage;
        //    this.Serenity += obstacle.SerenityDamage;
        //    EscapeFromObstacle(obstacle.gameObject);
        //}
        if (collision.gameObject.CompareTag("Food"))
        {
            var food = collision.gameObject.GetComponent<FoodData>();
            //agent.isStopped = true;
            this.Satiation += food.saturationRest;
            this.Nourishment += food.nourishmentRest;
            this.Health -= food.healthDamage;
            GameObject.Destroy(collision.gameObject);
        }
    }

    protected void EndGame() {
        float time = Time.time;
        defeatText.text = $"Koniec gry. \n Przetrwałeś {Math.Round(time)} sekund.";
        defeatPanel.SetActive(true);
        Time.timeScale = 0;
    }

    //private void EscapeFromObstacle(GameObject obstacle) {
    //    agent.isStopped = true;
    //    Vector3 dirToPlayer = (transform.position - obstacle.transform.position) * 2;
    //    Vector3 newDestination = transform.position + dirToPlayer;

    //    agent.isStopped = false;
    //    agent.SetDestination(newDestination);
    //}
}
