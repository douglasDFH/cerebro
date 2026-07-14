<?php

http_response_code(200);
$data = ['token ' => '1234567890'];
echo json_encode($data);
