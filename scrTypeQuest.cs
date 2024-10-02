using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class scrTypeQuest 
{
    public TipoMissao tipoMissao;

    public Recompensas recompensas;

    public int necessario;
    public int obtido=0;

//talvez de merda por conta do obtido--, arrumar outro lugar ou uma funcao mais abrangente pra missaocumprida()
    public bool MissaoCumprida()
    {
        return (obtido >= necessario);
    }

#region Condições missão
    public void ItemEntregue()
    {
        if(tipoMissao==TipoMissao.Falar)
        {
            obtido++;
            Debug.Log("falar obtido= " + obtido);

        }
    }

    public void ItemAtivo()
    {
        //condição do ativar é saber se o artefeto foi destruido
        if (tipoMissao==TipoMissao.Buscar || tipoMissao==TipoMissao.Presentear)
        {
            obtido++;
            Debug.Log("buscar obtido= " + obtido);
        }
    }
#endregion    

#region Recompensas

    public void recompensar()
    {
       KarmaRecompensa();
       CilindroRecompensa();
       AlicateRecompensa();
       ChaveRecompensa();
    }

    public void KarmaRecompensa()
    {
        if (recompensas==Recompensas.Karma)
        {
        scrGlobal.karma++;
        }
    }

    public void CilindroRecompensa()
    {
        if (recompensas==Recompensas.Cilindro)
        {
        scrGlobal.karma++;
        scrGlobal.cilindro++;
            
        }
    }

    public void AlicateRecompensa()
    {
        if (recompensas==Recompensas.Alicate)
        {
        scrGlobal.karma++;
        scrGlobal.alicate=true;
            
        }
    }

    public void ChaveRecompensa()
    {
        if (recompensas==Recompensas.Chave)
        {
        scrGlobal.karma++;
        scrGlobal.chave=true;
        }
    }

#endregion
}

public enum TipoMissao
{
    Buscar,
    Falar,
    Escolha,
    Presentear
}

public enum Recompensas
{
    Cilindro,
    Alicate,
    Chave,
    Karma
}
