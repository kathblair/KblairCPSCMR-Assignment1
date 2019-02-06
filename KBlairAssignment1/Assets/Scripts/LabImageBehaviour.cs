using UnityEngine;
using EasyAR;

namespace LabApp
{
    public class LabImageBehaviour : ImageTargetBehaviour
    {
        public Cardinal1 cardinal;
        public GameObject NestItems;
        public string targetName = "nest";

        protected override void Awake()
        {
            base.Awake();
            TargetFound += OnTargetFound;
            TargetLost += OnTargetLost;
            TargetLoad += OnTargetLoad;
            TargetUnload += OnTargetUnload;
            //hide all the nest items when you start.
            NestItems.gameObject.SetActive(false);
        }

        void OnTargetFound(TargetAbstractBehaviour behaviour)
        {
            Debug.Log("Found: " + Target.Id);
            Debug.Log("Found: " + Target.Name);
            if (Target.Name == targetName) // will need to update 
            {
                print("found sphere tracker == tracker2");
                NestItems.gameObject.SetActive(true);
                cardinal.GetComponent<Cardinal1>().CardinalEasyAR();
                //maybe I should reset it

            }
        }

        void OnTargetLost(TargetAbstractBehaviour behaviour)
        {
            Debug.Log("Lost: " + Target.Id);
            if (Target.Name == targetName) // will need to update 
            {
                print("lost sphere tracker == tracker2");
                NestItems.gameObject.SetActive(false);

            }
        }

        void OnTargetLoad(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
        {
            Debug.Log("Load target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
        }

        void OnTargetUnload(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
        {
            Debug.Log("Unload target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
        }
    }
}