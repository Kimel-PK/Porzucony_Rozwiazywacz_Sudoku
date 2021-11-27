using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sudoku : MonoBehaviour {

	public Transform rodzicCyfr;
	public bool wprowadzanie = true;
	public GameObject objektAnimacji;
	public Transform pion;
	public Transform poziom;
	public Transform środek;
	public Transform marker;
	public Slider suwak;

	public int[,] tablica = new int[9, 9];
	public Text[,] cyfry = new Text[9, 9];

	private int pozycjaMarkeraX = 0;
	private int pozycjaMarkeraY = 0;
	private int krokiProgramu = 0;
	private bool programDziała = false;
	private int wyjście = 0;
	private int wstawioneCyfry = 0;
	private float szybkośćAnimacji = 1;
	private bool pomińAnimacje = false;

	void Start () {

		for (int y = 0; y < 9; y++) {
			for (int x = 0; x < 9; x++) {
				cyfry [x, y] = rodzicCyfr.GetChild (y * 9 + x).GetComponent<Text> ();
			}
		}

	}

	void Update () {

		WyświetlCyfry ();
		if (suwak.value < 11) {
			szybkośćAnimacji = 1 / (suwak.value * suwak.value);
			pomińAnimacje = false;
		}
		else
			pomińAnimacje = true;

		if (wprowadzanie) {
			
			if (Input.GetButtonDown ("Dół")) {
				marker.Translate (Vector2.down * 100);
				pozycjaMarkeraY += 1;
			}
			if (Input.GetButtonDown ("Góra")) {
				marker.Translate (Vector2.up * 100);
				pozycjaMarkeraY -= 1;
			}
			if (Input.GetButtonDown ("Lewo")) {
				marker.Translate (Vector2.left * 100);
				;
				pozycjaMarkeraX -= 1;
			}
			if (Input.GetButtonDown ("Prawo")) {
				marker.Translate (Vector2.right * 100);
				pozycjaMarkeraX += 1;
			}

			if (pozycjaMarkeraX < 0) {
				marker.Translate (Vector2.right * 100);
				pozycjaMarkeraX = 0;
			}
			if (pozycjaMarkeraX > 8) {
				marker.Translate (Vector2.left * 100);
				pozycjaMarkeraX = 8;
			}
			if (pozycjaMarkeraY < 0) {
				marker.Translate (Vector2.down * 100);
				pozycjaMarkeraY = 0;
			}
			if (pozycjaMarkeraY > 8) {
				marker.Translate (Vector2.up * 100);
				pozycjaMarkeraY = 8;
			}

			if (Input.GetButtonDown ("1")) {
				tablica [pozycjaMarkeraX, pozycjaMarkeraY] = 1;
				cyfry [pozycjaMarkeraX, pozycjaMarkeraY].color = Color.black;
			}
			if (Input.GetButtonDown ("2")) {
				tablica [pozycjaMarkeraX, pozycjaMarkeraY] = 2;
				cyfry [pozycjaMarkeraX, pozycjaMarkeraY].color = Color.black;
			}
			if (Input.GetButtonDown ("3")) {
				tablica [pozycjaMarkeraX, pozycjaMarkeraY] = 3;
				cyfry [pozycjaMarkeraX, pozycjaMarkeraY].color = Color.black;
			}
			if (Input.GetButtonDown ("4")) {
				tablica [pozycjaMarkeraX, pozycjaMarkeraY] = 4;
				cyfry [pozycjaMarkeraX, pozycjaMarkeraY].color = Color.black;
			}
			if (Input.GetButtonDown ("5")) {
				tablica [pozycjaMarkeraX, pozycjaMarkeraY] = 5;
				cyfry [pozycjaMarkeraX, pozycjaMarkeraY].color = Color.black;
			}
			if (Input.GetButtonDown ("6")) {
				tablica [pozycjaMarkeraX, pozycjaMarkeraY] = 6;
				cyfry [pozycjaMarkeraX, pozycjaMarkeraY].color = Color.black;
			}
			if (Input.GetButtonDown ("7")) {
				tablica [pozycjaMarkeraX, pozycjaMarkeraY] = 7;
				cyfry [pozycjaMarkeraX, pozycjaMarkeraY].color = Color.black;
			}
			if (Input.GetButtonDown ("8")) {
				tablica [pozycjaMarkeraX, pozycjaMarkeraY] = 8;
				cyfry [pozycjaMarkeraX, pozycjaMarkeraY].color = Color.black;
			}
			if (Input.GetButtonDown ("9")) {
				tablica [pozycjaMarkeraX, pozycjaMarkeraY] = 9;
				cyfry [pozycjaMarkeraX, pozycjaMarkeraY].color = Color.black;
			}
			if (Input.GetButtonDown ("0")) {
				tablica [pozycjaMarkeraX, pozycjaMarkeraY] = 0;
				cyfry [pozycjaMarkeraX, pozycjaMarkeraY].color = Color.black;
			}
		}
		

	}

	void WyświetlCyfry () {

		for (int y = 0; y < 9; y++) {
			for (int x = 0; x < 9; x++) {
				if (tablica [x, y] != 0)
					cyfry [x, y].text = tablica [x, y].ToString ();
				else
					cyfry [x, y].text = " ";
			}
		}

	}

	public void RozwiążSudoku () {
		if (programDziała == false)
			StartCoroutine ("GłównaPętla");
		wprowadzanie = false;
		marker.gameObject.SetActive (false);

	}

	IEnumerator GłównaPętla () {

		programDziała = true;
		krokiProgramu = 0;

		while (true) {
			
			objektAnimacji.SetActive (true);
			wstawioneCyfry = 0;

			// kolumny

			pion.gameObject.SetActive (true);
			poziom.gameObject.SetActive (false);
			środek.gameObject.SetActive (false);

			for (int x = 0; x < 9; x++) {
				pion.transform.localPosition = new Vector3 (x * 100f, 400f, 0f);
				if (MożliwościWKolumnie (x) == 1) {
					
					// dopełnij kolumne

					int miejsceZerowe = -1;
					int brakującaCyfra = -1;

					pion.gameObject.SetActive (true);
					poziom.gameObject.SetActive (false);
					środek.gameObject.SetActive (true);

					for (int cyfra = 0; cyfra < 10; cyfra++) {
						for (int y = 0; y < 9; y++) {
							środek.localPosition = new Vector3 (x * 100f, 800f - y * 100f, 0f);
							if (!pomińAnimacje)
								yield return new WaitForSecondsRealtime (szybkośćAnimacji);
							if (tablica [x, y] == cyfra) {
								if (cyfra == 0) {
									miejsceZerowe = y;
									break;
								} else
									break;
							}
							if (y == 8) {
								brakującaCyfra = cyfra;
								break;
							}
						}
						if (brakującaCyfra != -1)
							break;
					}
						
					pion.gameObject.SetActive (false);
					środek.gameObject.SetActive (false);
					tablica [x, miejsceZerowe] = brakującaCyfra;
					wstawioneCyfry += 1;
					cyfry [x, miejsceZerowe].color = Color.blue;

				}
				if (!pomińAnimacje)
					yield return new WaitForSecondsRealtime (szybkośćAnimacji);

			}
				
			// wiersze

			pion.gameObject.SetActive (false);
			poziom.gameObject.SetActive (true);
			środek.gameObject.SetActive (false);

			for (int y = 0; y < 9; y++) {
				poziom.transform.localPosition = new Vector3 (400f, 800f - y * 100f, 0f);
				if (MożliwościWRzędzie (y) == 1) {

					// dopełnij rząd

					int miejsceZerowe = -1;
					int brakującaCyfra = -1;

					pion.gameObject.SetActive (false);
					poziom.gameObject.SetActive (true);
					środek.gameObject.SetActive (true);

					for (int cyfra = 0; cyfra < 10; cyfra++) {
						for (int x = 0; x < 9; x++) {
							środek.localPosition = new Vector3 (x * 100f, 800f - y * 100f, 0f);
							if (!pomińAnimacje)
								yield return new WaitForSecondsRealtime (szybkośćAnimacji);
							if (tablica [x, y] == cyfra) {
								if (cyfra == 0) {
									miejsceZerowe = x;
									break;
								} else
									break;
							}
							if (x == 8) {
								brakującaCyfra = cyfra;
								break;
							}
						}
						if (brakującaCyfra != -1)
							break;
					}

					poziom.gameObject.SetActive (false);
					środek.gameObject.SetActive (false);
					tablica [miejsceZerowe, y] = brakującaCyfra;
					wstawioneCyfry += 1;
					cyfry [miejsceZerowe, y].color = Color.blue;

				}
				if (!pomińAnimacje)
					yield return new WaitForSecondsRealtime (szybkośćAnimacji);

			}

			// kwadraty

			pion.gameObject.SetActive (true);
			poziom.gameObject.SetActive (true);
			środek.gameObject.SetActive (true);

			bool wyjdź = false;

			for (int KwY = 0; KwY < 3; KwY++) {
				for (int KwX = 0; KwX < 3; KwX++) {

					if (MożliwościWKwadracie (KwX, KwY) != 0) {
						
						if (MożliwościWKwadracie (KwX, KwY) == 1) {

							int miejsceZeroweX = -1;
							int miejsceZeroweY = -1;
							int brakującaCyfra = -1;

							for (int cyfra = 0; cyfra < 10; cyfra++) {
								for (int y = KwY * 3; y < KwY * 3 + 3; y++) {
									poziom.localPosition = new Vector3 (400f, 800f - y * 100f, 0f);
									for (int x = KwX * 3; x < KwX * 3 + 3; x++) {
										pion.localPosition = new Vector3 (x * 100f, 400f, 0f);
										środek.localPosition = new Vector3 (x * 100f, 800f - y * 100f, 0f);
										if (!pomińAnimacje)
											yield return new WaitForSecondsRealtime (szybkośćAnimacji);
										if (tablica [x, y] == cyfra) {
											if (cyfra == 0) {
												miejsceZeroweX = x;
												miejsceZeroweY = y;
												wyjdź = true;
												break;
											} else {
												wyjdź = true;
												break;
											}
										}
									}
									if (wyjdź) {
										wyjdź = false;
										break;
									}
									if (y == KwY * 3 + 2) {
										brakującaCyfra = cyfra;
										break;
									}
								}
								if (brakującaCyfra != -1)
									break;
							}

							tablica [miejsceZeroweX, miejsceZeroweY] = brakującaCyfra;
							wstawioneCyfry += 1;
							cyfry [miejsceZeroweX, miejsceZeroweY].color = Color.blue;

						} else {
							// testuj kwadrat 3 x 3 na występowanie danej cyfry

							for (int cyfra = 1; cyfra < 10; cyfra++) {

								// definiowanie tablicy możliwości

								bool[,] możliwości = new bool[3, 3];
								for (int y = 0; y < 3; y++) {
									for (int x = 0; x < 3; x++)
										możliwości [x, y] = true;
								}

								// sprawdzanie czy dana liczba jest już w kwadracie

								poziom.gameObject.SetActive (true);
								pion.gameObject.SetActive (true);
								środek.gameObject.SetActive (true);

								bool wystąpiła = false;

								for (int y = KwY * 3; y < KwY * 3 + 3; y++) {
									poziom.localPosition = new Vector3 (400f, 800f - y * 100f, 0f);
									for (int x = KwX * 3; x < KwX * 3 + 3; x++) {
										pion.localPosition = new Vector3 (x * 100f, 400f, 0f);
										środek.localPosition = new Vector3 (x * 100f, 800f - y * 100f, 0f);
										if (!pomińAnimacje)
											yield return new WaitForSecondsRealtime (szybkośćAnimacji);

										if (tablica [x, y] == cyfra)
											wystąpiła = true;
									}
								}

								if (!wystąpiła) {

									// usuwanie z możliwości zapełnionych pól

									for (int y = KwY * 3; y < KwY * 3 + 3; y++) {
										for (int x = KwX * 3; x < KwX * 3 + 3; x++) {
											if (tablica [x, y] != 0) {
												możliwości [x - KwX * 3, y - KwY * 3] = false;
											}
										}
									}

									// wyrzucanie z możliwości kolumn

									poziom.gameObject.SetActive (false);
									pion.gameObject.SetActive (true);
									środek.gameObject.SetActive (false);

									for (int x = KwX * 3; x < KwX * 3 + 3; x++) {
										pion.localPosition = new Vector3 (x * 100f, 400f, 0f);
										if (!pomińAnimacje)
											yield return new WaitForSecondsRealtime (szybkośćAnimacji);

										if (CzyCyfraWKolumnie (x, cyfra)) {
											możliwości [KwX * -3 + x, 0] = false;
											możliwości [KwX * -3 + x, 1] = false;
											możliwości [KwX * -3 + x, 2] = false;
										}
									}

									// wyrzucanie z możliwości wierszy

									poziom.gameObject.SetActive (true);
									pion.gameObject.SetActive (false);
									środek.gameObject.SetActive (false);

									for (int y = KwY * 3; y < KwY * 3 + 3; y++) {
										poziom.localPosition = new Vector3 (400f, 800f - y * 100f, 0f);
										if (!pomińAnimacje)
											yield return new WaitForSecondsRealtime (szybkośćAnimacji);

										if (CzyCyfraWRzędzie (y, cyfra)) {
											możliwości [0, KwY * -3 + y] = false;
											możliwości [1, KwY * -3 + y] = false;
											możliwości [2, KwY * -3 + y] = false;
										}
									}

									// podliczanie możliwości

									int ileMożliwości = 0;

									for (int y = 0; y < 3; y++) {
										for (int x = 0; x < 3; x++) {
											if (możliwości [x, y] == true)
												ileMożliwości += 1;
										}
									}

									// wpisywanie pewnej cyfry

									if (ileMożliwości == 1) {
										for (int y = 0; y < 3; y++) {
											for (int x = 0; x < 3; x++) {
												if (możliwości [x, y] == true) {
													tablica [KwX * 3 + x, KwY * 3 + y] = cyfra;
													wstawioneCyfry += 1;
													cyfry [KwX * 3 + x, KwY * 3 + y].color = Color.blue;
												}
											}
										}
									}

								}

							}

						}

					}


				}
			}
				
			krokiProgramu += 1;

			if (wstawioneCyfry == 0) {
				Debug.Log ("Programowi brakuje dedukcji");
				break;
			}

			if (MożliwościWKwadracie (0, 0) == 0 && MożliwościWKwadracie (0, 1) == 0 && MożliwościWKwadracie (0, 2) == 0 && MożliwościWKwadracie (1, 0) == 0 && MożliwościWKwadracie (1, 1) == 0 && MożliwościWKwadracie (1, 2) == 0 && MożliwościWKwadracie (2, 0) == 0 && MożliwościWKwadracie (2, 1) == 0 && MożliwościWKwadracie (2, 2) == 0)
				break;

		}

		Debug.Log ("Koniec programu");
		objektAnimacji.SetActive (false);
		marker.gameObject.SetActive (true);
		wprowadzanie = true;
		yield return null;

		programDziała = false;
		Debug.Log ("Wykonano w " + krokiProgramu + " pełnych pętlach");

	}

	int MożliwościWKolumnie (int nrKolumny) {

		wyjście = 0;

		for (int k = 0; k < 9; k++) {
			if (tablica [nrKolumny, k] == 0)
				wyjście += 1;
		}

		return wyjście;

	}

	bool CzyCyfraWKolumnie (int nrKolumny, int cyfra) {

		for (int y = 0; y < 9; y++) {
			if (tablica [nrKolumny, y] == cyfra)
				return true;
		}

		return false;

	}

	int MożliwościWRzędzie (int nrRzędu) {

		wyjście = 0;

		for (int r = 0; r < 9; r++) {
			if (tablica [r, nrRzędu] == 0)
				wyjście += 1;
		}

		return wyjście;

	}

	bool CzyCyfraWRzędzie (int nrRzędu, int cyfra) {

		for (int x = 0; x < 9; x++) {
			if (tablica [x, nrRzędu] == cyfra)
				return true;
		}

		return false;

	}

	int MożliwościWKwadracie (int KwX, int KwY) {

		wyjście = 0;

		for (int y = 0; y < 3; y++) {
			for (int x = 0; x < 3; x++) {
				if (tablica [KwX * 3 + x, KwY * 3 + y] == 0)
					wyjście += 1;
			}
		}
			
		return wyjście;

	}

	public void Stop () {

		StopAllCoroutines ();
		programDziała = false;
		pion.gameObject.SetActive (false);
		poziom.gameObject.SetActive (false);
		środek.gameObject.SetActive (false);
		krokiProgramu = 0;
		marker.gameObject.SetActive (true);
		wprowadzanie = true;

	}

	public void Reset () {

		tablica = new int[9,9];
		marker.gameObject.SetActive (true);
		wprowadzanie = true;

	}

}
