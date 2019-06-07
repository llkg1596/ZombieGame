using System.Collections;
using UnityEngine;

// 총을 구현한다
public class Gun : MonoBehaviour {
    // 총의 상태를 표현하는데 사용할 타입을 선언한다
    public enum State {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }

    public State state { get; private set; } // 현재 총의 상태

    public Transform fireTransform; // 총알이 발사될 위치

    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과
    public ParticleSystem shellEjectEffect; // 탄피 배출 효과

    private LineRenderer bulletLineRenderer; // 총알 궤적을 그리기 위한 렌더러

    private AudioSource gunAudioPlayer; // 총 소리 재생기
    public AudioClip shotClip; // 발사 소리
    public AudioClip reloadClip; // 재장전 소리

    public float damage = 25; // 공격력
    private float fireDistance = 50f; // 사정거리

    public int ammoRemain = 100; // 남은 전체 탄약
    public int magCapacity = 25; // 탄창 용량
    public int magAmmo; // 현재 탄창에 남아있는 탄약


    public float timeBetFire = 0.12f; // 총알 발사 간격
    public float reloadTime = 1.8f; // 재장전 소요 시간
    private float lastFireTime; // 총을 마지막으로 발사한 시점


    private void Awake() {
        // 사용할 컴포넌트들의 참조를 가져오기
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        // 사용할 점을 두 개 지정한다
        // 첫 번째 점에는 총구위치
        // 두 번째 점에는 탄알이 닿을 위치를 지정한다
        bulletLineRenderer.positionCount = 2;

        //궤적 비활성화
        bulletLineRenderer.enabled = false;

    }

    // 총 상태 초기화
    private void OnEnable() {
        // 탄창 채우기
        magAmmo = magCapacity;

        // 상태변경 : Ready
        state = State.Ready;

        // 마지막 발화시점 초기화
        lastFireTime = 0;
        
    }

    // 발사 시도
    public void Fire() {
        // 현재 발사 가능한 상태
        // 그리고 timeBetFire (총알 사이의 발사 간격 시간) 이상의 시간이 지난 상태
        if(state == State.Ready && Time.time >= lastFireTime + timeBetFire)
        {
            // 마지막 발사 시점 갱신
            lastFireTime = Time.time;
            // 발사
            Shot();
        }
    }

    // 실제 발사 처리
    private void Shot() {

        // hit : 레이캐스트를 저장할 변수
        RaycastHit hit;
        // hitPosition : 탄알이 맞은 장소를 저장하는 변수
        Vector3 hitPosition = Vector3.zero;

        // 레이를 쏴서 충돌하는 오브젝트가 있으면 true
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            // 충돌한 물체로 부터 IDamageable 오브젝트를 가져온다
            // target 변수에 저장
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            //오브젝트를 가져오는데 성공했다면
            if(target != null)
            {
                // 충돌한 물체의 OnDamage 함수를 실행하여 값을 입력한다
                // 데미지값, 탄알이 맞은 위치, 탄알이 맞은 표면 방향 값을 저장한다
                target.OnDamage(damage, hit.point, hit.normal);

            }

            // 레이가 충돌한 위치를 새롭게 업데이트
            hitPosition = hit.point;
        }

        // 레이가 다른 물체와 충돌하지 않았다면
        else
        {
            // 충돌한 위치를 탄알의 최대 사정거리 위치로 업데이트 한다
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;

        }

        // 발사 이펙트를 시작
        // 탄알의 충돌위치를 입력으로 넘겨준다
        StartCoroutine(ShotEffect(hitPosition));

        // 총알 하나씩 소비
        magAmmo--;
        if(magAmmo <= 0)
        {
            // 총알을 다 사용했으면 Empty 상태로 진입한다
            state = State.Empty;

        }
    }

    // 발사 이펙트와 소리를 재생하고 총알 궤적을 그린다
    private IEnumerator ShotEffect(Vector3 hitPosition) {

        // 총구 화염 효과
        muzzleFlashEffect.Play();
        // 탄피 효과
        shellEjectEffect.Play();

        // 총격 소리 재생
        gunAudioPlayer.PlayOneShot(shotClip);

        // 첫번째 시작점은 총구의 위치
        bulletLineRenderer.SetPosition(0, fireTransform.position);
        // 두번째 끝점은 충돌위치
        bulletLineRenderer.SetPosition(1, hitPosition);

        // 궤적 활성화
        bulletLineRenderer.enabled = true;

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 총알 궤적을 지운다
        bulletLineRenderer.enabled = false;
    }

    // 재장전 시도
    public bool Reload() {

        // 재장전을 하지 않는 경우
        // 이미 재장전 중인 경우
        // 재장전에 사용할 남은 탄알이 없는 경우
        // 탄창이 꽉 차 있는 경우
        if(state == State.Reloading || ammoRemain <= 0 || magAmmo >= magCapacity)
        {
            return false;
        }

        // 재장전 코루틴 실행
        StartCoroutine(ReloadRoutine());

        return true;

    }

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine() {

        // 재장전 상태로 진입
        state = State.Reloading;
        // 재장전 오디오 재생
        gunAudioPlayer.PlayOneShot(reloadClip);

        // 재장전 시간만큼 interval
        yield return new WaitForSeconds(reloadTime);

        // ammoToFill : 재장전 해야하는 총알 수
        // = 탄창 용량 - 현재 총알 수
        int ammoToFill = magCapacity - magAmmo;

        // 만약 남은 총알 보다 재장전 해야 하는 총알 수가 더 많다면
        if(ammoRemain < ammoToFill)
        {
            ammoToFill = ammoRemain;
        }

        // 현재 총알 수 증가
        magAmmo += ammoToFill;
        // 남은 총알 수 감소
        ammoRemain -= ammoToFill;

        // 준비상태로 진입
        state = State.Ready;

    }
}