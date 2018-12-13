//-----------------------------------------------------------------------
//Shoot a Dice
//https://github.com/tomitomi3/Make
//
//History
//2018.07.08
//-----------------------------------------------------------------------
void setup() {
  Serial.begin(9600);
  //output
  pinMode(13, OUTPUT);
  pinMode(8, OUTPUT);

  //check
  pinMode(LED_BUILTIN, OUTPUT);
  for ( int i = 0; i < 5; i++)
  {
    digitalWrite(LED_BUILTIN, HIGH);
    delay(200);
    digitalWrite(LED_BUILTIN, LOW);
    delay(300);
  }

  //debug
  digitalWrite(13, HIGH);
  digitalWrite(8, HIGH);
  delay(50);
  digitalWrite(13, LOW);
  digitalWrite(8, LOW);

}

void ON(int dtime)
{
  digitalWrite(13, HIGH);
  digitalWrite(8, HIGH);
  delay(dtime);
  digitalWrite(13, LOW);
  digitalWrite(8, LOW);
}

void loop() {
  while (Serial.available() > 0)
  {
    byte InputData = Serial.read();

    //receve
    switch (InputData)
    {
      case '1' :
        ON(1);
        break;
      case '2' :
        ON(2);
        break;
      case '3' :
        ON(3);
        break;
      case '4' :
        ON(4);
        break;
      case '5' :
        ON(5);
        break;
      case '6' :
        ON(6);
        break;
      case '7' :
        ON(7);
        break;
      case '8' :
        ON(8);
        break;
      case '9' :
        ON(9);
        break;
      case 'a' :
        ON(10);
        break;
      case 'b' :
        ON(11);
        break;
      case 'c' :
        ON(12);
        break;
      case 'd' :
        ON(13);
        break;
      case 'e' :
        ON(14);
        break;
      case 'f' :
        ON(15);
        break;
      case 'g' :
        ON(16);
        break;
      case 'h' :
        ON(17);
        break;
      case 'i' :
        ON(18);
        break;
      case 'j' :
        ON(19);
        break;
      case 'k' :
        ON(20);
        break;
      case 'l' :
        ON(21);
        break;
      case 'm' :
        ON(22);
        break;
      case 'n' :
        ON(23);
        break;
      case 'o' :
        ON(24);
        break;
      case 'p' :
        ON(25);
        break;
      default :
        ON(13);
        break;
    }

    if (InputData == 'a') {
    }

    //receve 'b' for debug
    if (InputData == 'b') {
      //digitalWrite(LED_BUILTIN, LOW);
    }
  }
}
