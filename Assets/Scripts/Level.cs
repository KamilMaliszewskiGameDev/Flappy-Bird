using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private const float PIPE_WIDTH = 7.8f;
    private const float PIPE_HEAD_HEIGHT = 3.85f;
    private const float CAMERA_ORTHO_SIZE = 50f;
    private const float PIPE_MOVE_SPEED = 30f;
    private const float PIPE_DESTROY_X_POSITION = -150f;

    private List<Pipe> pipeList;

    private void Awake(){
        pipeList = new List<Pipe>();
    }

    private void Start(){
        CreateGapPipes(50f, 20f, 20f);
    }

    private void Update(){
        HandlePipeMovement();
    }
    

    private void HandlePipeMovement(){
        for(int i=0; i<pipeList.Count; i++){
            Pipe pipe = pipeList[i];
            pipe.Move();
            if(pipe.GetXPosition() < PIPE_DESTROY_X_POSITION){
                pipe.DestroySelf();
                pipeList.Remove(pipe);
                i--;
            }

        }
    }

    private void CreateGapPipes(float gapY, float gapSize, float posX){
        CreatePipe(gapY - gapSize * 0.5f, posX, true);
        CreatePipe(CAMERA_ORTHO_SIZE * 2f - gapY - gapSize * 0.5f, posX, false);
    }

    private void CreatePipe(float height, float posX, bool createBottom){
        //setup pipe head
        Transform pipeHead = Instantiate(GameAssets.GetInstance().pfPipeHead);

        float pipeHeadPosY;
        if(createBottom){
            pipeHeadPosY = -CAMERA_ORTHO_SIZE + height - PIPE_HEAD_HEIGHT * 0.5f;
        } else {
            pipeHeadPosY = +CAMERA_ORTHO_SIZE - height - 5f + PIPE_HEAD_HEIGHT * 0.5f;
        }
        
        pipeHead.position = new Vector3(posX, pipeHeadPosY);

        //setup pipe body
        Transform pipeBody = Instantiate(GameAssets.GetInstance().pfPipeBody);

        float pipeBodyPosY;
        if(createBottom){
            pipeBodyPosY = -CAMERA_ORTHO_SIZE;
        } else {
            pipeBodyPosY = +CAMERA_ORTHO_SIZE;
            pipeBody.localScale = new Vector3(1, -1, 1);
        }
        pipeBody.position = new Vector3(posX, pipeBodyPosY);

        SpriteRenderer pipeBodySpriteRenderer = pipeBody.GetComponent<SpriteRenderer>();
        pipeBodySpriteRenderer.size = new Vector2(PIPE_WIDTH, height);

        BoxCollider2D pipeBodyBoxCollider = pipeBody.GetComponent<BoxCollider2D>();
        pipeBodyBoxCollider.size = new Vector2(PIPE_WIDTH, height);
        pipeBodyBoxCollider.offset = new Vector2(0f, height * 0.5f);

        Pipe pipe = new Pipe(pipeHead, pipeBody);
        pipeList.Add(pipe);
    }

    private class Pipe {
        private Transform pipeHeadTransform;
        private Transform pipeBodyTransform;

        public Pipe(Transform pipeHeadTransform, Transform pipeBodyTransform){
            this.pipeBodyTransform = pipeBodyTransform;
            this.pipeHeadTransform = pipeHeadTransform;
        }

        public void Move(){
            pipeHeadTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;
            pipeBodyTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;
        }

        public float GetXPosition(){
            return pipeHeadTransform.position.x;
        }

        public void DestroySelf(){
            Destroy(pipeHeadTransform.gameObject);
            Destroy(pipeBodyTransform.gameObject);
        }
    }
}
