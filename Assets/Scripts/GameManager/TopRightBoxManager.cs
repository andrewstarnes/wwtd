using UnityEngine;
using System.Collections;

public class TopRightBoxManager : MonoBehaviour {

	public UILabel cashLabel;
	public UILabel livesLabel;
	public UILabel wavesLabel;
	public int currentCashDisplay;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int cashDiff = currentCashDisplay-GameManager.REF.usersCash;
		if(cashDiff!=0) {
			int cashAdder = 0;
			if(cashDiff>100) {
				cashAdder = 25;
			} else
			if(cashDiff>50) {
				cashAdder = 10;
			} else 
			if(cashDiff>10) {
				cashAdder = 2;
			} else if(cashDiff>0) {
				cashAdder =1;
			} else if(cashDiff<-100) {
				cashAdder = -25;
			} else if(cashDiff<-50) {	
				cashAdder = -10;
			} else if(cashDiff<-10) {
				cashAdder = -2;
			} else if(cashDiff<0) {
				cashAdder = -1;
			}
			currentCashDisplay -= cashAdder;
			this.cashLabel.text = currentCashDisplay.ToString("C0");
		}
	}
}
