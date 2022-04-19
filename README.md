# Curious

## 소개

*Curious*는 분야별 질문/답변을 위한 커뮤니티입니다.  
단순히 답(솔루션)을 구하는 질문이 아니라 사람들과 이야기를 나누며 의견을 나누는 공간입니다.  
더욱이 인문계열의 학문에서 정해진 답은 없으며, 심도 있는 대화를 위해서는 대학원 진학 혹은 세미나 같은 고수준의 장벽이 존재합니다.  
이러한 장벽을 낮추기 위해 *Curious*와 같은 오픈 커뮤니티를 고안하였습니다.

![화면 (1)](https://user-images.githubusercontent.com/46207836/163927333-698a337f-4880-4ac9-a505-9959a6786b45.png)

## 주요 기능

### 💡 카테고리 분류

- 대분류: 계열
- 중분류: 학과
- 소분류: 세부 과목

### 💡 질문하기

- 카테고리에 따라 질문하기

### 💡 답변하기

- 계층형 댓글(최대 3계층)로 답변하기

### ERD

![board diagram](https://user-images.githubusercontent.com/46207836/163927032-99d64e44-f3f3-4465-a00c-53cdcdb8aaa3.png)

## UI

| 페이지    | 구성                                 |
| --------- | ------------------------------------ |
| 로그인    | 이메일, 비밀번호 입력                |
| 회원가입  | 이름, 이메일, 비밀번호 입력          |
| 질문 보기 | 헤더, 본문, 댓글 리스트, 답글 리스트 |
| 질문 작성 | 헤더, 카테고리 선택, 스마트 에디터   |
| 홈        | 헤더, 카테고리(대, 중, 소)별 검색    |

### 데이터베이스 설계 시 주의한 내용

#### 1. 패스워드 암호화

안전한 패스워드 저장을 위해 암호화를 사용하였습니다.
다양한 암호화 방식 중 솔트를 사용한 bcrypt로 Brute Force, Rainbow Table 공격 등을 막아낼 수 있도록 하였습니다.

```C#
var member = _context.Members
    .FromSqlInterpolated($"EXECUTE SP_SignInMember {memberRequest.MailAddress}")
    .AsEnumerable()
    .SingleOrDefault();
// 미등록된 이메일
if (member == null)
{
    return NotFound();
}


var verified = BCrypt.Net.BCrypt.Verify(memberRequest.MemberPassword, member.MemberPassword);
if (verified)
{
    return Ok(member);
}
else
{
    return BadRequest(new ErrorResponse(308, "Wrong Password"));
}
```

#### 2. 계층형

카테고리와 댓글을 계층형으로 구현하기 위해 Self Join을 위해 FK로 parent id 값을 저장하도록 설정하였습니다.  
프로토타입이기에 페이지네이션(Pagination)은 미적용상태입니다.

- 계층형 카테고리 불러오기

```C#
var categories = _context.Categories
    .Where(c => c.ParentCategoryId == null)
    .Include(c => c.InverseParentCategory)
    .ThenInclude(c => c.InverseParentCategory)
    .ToList();
```

- 특정 카테고리의 하위에 존재하는 질문 리스트를 최신순으로 불러오기

```C#
var questions = _context.Questions
   .AsNoTracking()
   .Where(q => q.Category.CategoryId == queryParameter.CategoryId
   || q.Category.ParentCategory.CategoryId == queryParameter.CategoryId
   || q.Category.ParentCategory.ParentCategory.CategoryId == queryParameter.CategoryId)
   .Include(q => q.Member)
   .OrderByDescending(q => q.QuestionId)
   .ToList();
```

- 특정 질문에 대한 답변 불러오기

```C#
var comments = _context.Comments
   .Where(c => c.QuestionId == questionId && c.Depth == 0)
   .Include(c => c.InverseParentComment)
   .ThenInclude(c => c.InverseParentComment)
   .ThenInclude(c => c.InverseParentComment)
   .ToList();
```

### 추가 이슈

- JWT 토큰
- 수정/삭제 기능
