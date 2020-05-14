using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMCtrl : MonoBehaviour
{
	private GameCtrl GM;
	private AudioSource Audio;

	public int playingBGM;
	public int ReadyToPlayBGM;// 一個暫存資料的容器

	public AudioClip[] BGM;

	void Start(){
		GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameCtrl>();
		Audio = GetComponent<AudioSource>();
		ChangeBGM(ReadyToPlayBGM);
		playingBGM = ReadyToPlayBGM;
	}

	// Update is called once per frame
	void Update(){
		if(Audio.time >= BGM[1].length-0.016 && playingBGM == 1){	//	if 1 ends, change to 2(loop mode).
			ReadyToPlayBGM = 2;
			ChangeBGM(ReadyToPlayBGM);
			playingBGM = ReadyToPlayBGM;
		}
		if(Audio.time >= BGM[2].length-0.016 && playingBGM == 2){	//	loop itself
			Audio.Play();
		}
		if(ReadyToPlayBGM != playingBGM){				//	check BGM and change.
			StartCoroutine(ChangeBGMfadeout(ReadyToPlayBGM));
			playingBGM = ReadyToPlayBGM;
		}
	}

	public void ChangeBGM(int BGMNum){
		Audio.Stop();
		this.GetComponent<AudioSource>().clip = BGM[BGMNum];
		Audio.Play();
	}
	private float buf;
	private float decrease_rate = 0.954992587f;
	public IEnumerator ChangeBGMfadeout(int index){				//	some music may want to fade out.
		buf = Audio.volume;
		for(int a = 0; a < 100; a++){
			Audio.volume *= decrease_rate;
			yield return new WaitForSeconds(0.01f);
		}
		Audio.Stop();
		yield return new WaitForSeconds(2);
		this.GetComponent<AudioSource>().clip = BGM[index];
		Audio.volume = buf;
		Audio.Play();
	}


}
