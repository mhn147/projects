<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <link rel="icon" type="image/png" href="./favicon.png" />
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous" />
  <link rel="stylesheet" href="css/main.css" />
  <link rel="stylesheet" href="css/contact.css" />
  <title>Nasri Mohamed | Contact</title>
</head>

<body>
  <header class="less-dark-background-color">
    <nav class="container navbar navbar-expand-lg navbar-dark p-1">
      <div class="container-fluid px-0">
        <a class="navbar-brand brand" href="index.php">
          <img src="img/logo.svg" class="brand-img" alt="Website Logo: Uppercase letter N" />
        </a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav me-auto mb-2 mb-lg-0 navbar-items-container">
            <li class="nav-item text-center mx-2">
              <a class="nav-link p-2" href="https://www.linkedin.com/in/mhnasri">
                <img src="img/LinkedIn-logo.svg" class="nav-icon" alt="LinkedIn profile Link">
              </a>
            </li>
            <li class="nav-item text-center mx-2">
              <a class="nav-link p-2" href="https://github.com/mhn147">
                <img src="img/github-white-logo.svg" class="nav-icon" alt="Github profile Link">
              </a>
            </li>
            <li class="nav-item text-center mx-2">
              <a class="nav-link p-2" href="https://twitter.com/mhn_147">
                <img src="img/twitter-logo.svg" class="nav-icon" alt="Twitter profile Link">
              </a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  </header>

  <main class="container my-5 animated animatedFadeInUp fadeInUp">
    <div class="row">
      <div class="col-12 hero-container">
        <section class="hero m-2 round-border text-white p-5 less-dark-background-color">
          <h1 class="text-center">Hi, How can I help you ? ;&#41;</h1>
        </section>
      </div>
      <div class="col-12">
        <div class="row d-flex justify-content-center">
          <div class="form-container col-12 col-sm-8 m-2 text-white p-5">
            <form action="mail.php" method="post" enctype="multipart/form-data">
              <div class="row">
                <div class="col-12 col-sm-6">
                  <div class="mb-3">
                    <label for="nameInput" class="form-label text-light">Name</label>
                    <input type="text" name="name" required class="form-control text-light" id="nameInput" aria-describedby="emailHelp" />
                  </div>
                </div>
                <div class="col-12 col-sm-6">
                  <div class="mb-3">
                    <label for="emailInput" class="form-label text-light">Email</label>
                    <input type="email" name="email" required class="form-control text-light" id="emailInput" aria-describedby="emailHelp" />
                  </div>
                </div>
              </div>
              <div class="mb-3">
                <label for="messageInput" class="form-label text-light">Message</label>
                <textarea name="message" required class="form-control text-light" id="messageInput" rows="8"></textarea>
              </div>
              <div class="text-center mt-5">
                <button type="submit" name="send" value="send" class="btn btn-outline-light p-2 cta-btn">
                  Submit
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </main>

  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/js/bootstrap.bundle.min.js" integrity="sha384-b5kHyXgcpbZJO/tY9Ul7kGkf1S0CWuKcCD38l8YkeH8z8QjE0GmW1gYU5S9FOnJ0" crossorigin="anonymous"></script>
</body>

</html>