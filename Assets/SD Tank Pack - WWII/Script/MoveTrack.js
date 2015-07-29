#pragma strict

var currentTTIdx : int = 0;

var trackTexturs : Texture[];

var speed : float = 40; //  km/h

var moveTick : float = 0.1;

var GearStatus : int = 0;


function Update () {

	GearStatus =1 ;
	speed = 3;

			if (speed < 1)
				speed = 1;
				
			if (Time.time > moveTick) {
				currentTTIdx++;
				if (currentTTIdx >= trackTexturs.Length)
					currentTTIdx = 0;
					
				GetComponent.<Renderer>().material.mainTexture = trackTexturs[currentTTIdx]; 
				
				// One Texture made 4cm move
				moveTick = Time.time + 4 / (speed * 1000 / (60 * 60) * 100);
			}

			
		

}