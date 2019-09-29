using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : BaseProjectileController
{
  public override Vector3 InitialSpeed() { return new Vector3(0, 5, 20); }
  public override bool UsesGravity() { return true; }
}
