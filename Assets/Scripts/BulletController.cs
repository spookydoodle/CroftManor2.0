using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : BaseProjectileController
{
  public override Vector3 InitialSpeed() { return new Vector3(0, 0, 20); }
  public override bool UsesGravity() { return false; }
}
