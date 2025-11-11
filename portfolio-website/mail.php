<?php 

    if (!empty($_POST["send"])) {
        $name = $_POST['name'];
        $email = $_POST['email'];
        $message = $_POST['message'];
        $content="From: $name \n Message: $message";
        $recipient = "muhammed.al.nasri@gmail.com";
        $subject = "[Porfolio Contact Form]";
        $headers = "From: $email \r\n";

        mail($recipient, $subject, $content, $headers) or die("Error!");
        header("Location: contact.php?mailsent");
    }
?>