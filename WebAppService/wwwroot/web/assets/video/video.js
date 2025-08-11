/*jslint plusplus: true, evil: true */
/*global $, console, alret, prompt, Element*/

// Get All My ELement 

var // Variables (Player)
    player = document.querySelector(".player"),
    video = document.querySelector(".viewer"),
    overlayLoad = document.querySelector('.player .overlay-load'),
    // Variables (Controls)
    playerControl = document.querySelector('.player .player-controls'),
    hideControl = document.querySelector('.player .hide-controls'),
    btnTogglePlay = document.querySelector(".player .player-controls > .btnPlay"),
    showTilteVideo = document.querySelector('.player .show-title-video'),
    // Variables (Controls) Advanced Option
    overlayPlay = document.querySelector('.overlay-play'),
    showElementByEvent = document.querySelector('.player .viewEventNow'),
    // Variables (Controls) Progress
    progress = document.querySelector(".progress"),
    progressLoad = document.querySelector('.player .player-controls .progress-load'),
    progressBar = document.querySelector(".progress-filled"),
    progressBall = document.querySelector(".progress-ball"),
    // Variables (Controls) Show Time in Progress
    showTime = document.querySelector(".player .player-controls > .time"),
    // Variables (Controls) Show Time and Your Position On Move Progress
    screenMove = document.querySelector('.player .screen-move'),
    videoMove = document.querySelector('.player .screen-move .video-move'),
    screenTime = document.querySelector('.player .screen-move .time-screen'),
    // Variables (Controls) Ranges Volume And playBackRate
    ranges = document.querySelectorAll(".player-slider"),
    rangeVolume = document.querySelector(".player input[name='volume']"),
    // Variables (Controls) volume
    volume = document.querySelector(".player .volume"),
    iconVolume = document.querySelector(".player .volume .icon"),
    buttonVolume = document.querySelector(".player .volume i.fa-volume-up"),
    iconCloseVolume = document.querySelector(".player .volume i.fa-close"),
    volumeVal = '',
    // Variables (Controls) Speed[playbackRate]
    // Variables (Controls) Player Option 
    btnOptionPlayer = document.querySelector('.player .player-controls .player-option .button-option'),
    autoplay = document.querySelector('.player .player-controls .player-option .autoplay .range-auto'),
    optionItem = document.querySelector('.player .player-controls .player-option .option-item'),
    rangePlaybackRate = document.querySelector(".player input[name='playbackRate']"),
    playBackRate = document.querySelector('.player .speed .play-back-rate'),
    viewPlayBackRate = document.querySelector('.player .speed .show-play-back'),
    qualityOption = document.querySelector('.player .player-controls .player-option .quality .range-quality span'),
    qualityOptionAuto = document.querySelector('.player .player-controls .player-option .quality .auto-quality .click-active'),
    timeVideoNow,
    // Variables (Controls) Full Screen
    btnFullScreen = document.querySelector('.player .button-full-screen'),
    msgFullScreen = document.querySelector('.player .msg-full-screen'),

    // Variables (PLaylist) Element HTML
    btnBackward = document.querySelector('.player .player-controls .backward'),
    btnForward = document.querySelector('.player .player-controls .forward'),
    viewNext_Prev = document.querySelector('.player .player-controls .view-next-prev'),
    viewNext_PrevDesc = document.querySelector('.player .player-controls .view-next-prev .desc'),
    viewNext_PrevTime = document.querySelector('.player .player-controls .view-next-prev .time'),
    btnsOptionPlaylist = document.querySelectorAll('.player-play-list .play-list .options .option .btn-option'),
    btnOrder = document.querySelector('.player-play-list .play-list .options .option .btn-option.order'),
    btnRepeat = document.querySelector('.player-play-list .play-list .options .option .btn-option.repeat'),
    videoPlaylist = document.querySelectorAll('.player-play-list .play-list ul li .video-list'),
    linkVideo = document.querySelectorAll('.player-play-list .play-list ul li'),
    countNumPlaylist = document.querySelectorAll('.player-play-list .play-list ul li .count'),
    viewTimePlaylist = document.querySelectorAll('.player-play-list .play-list ul li .time'),
    pathPlaylist = [
        'https://www.w3schools.com/tags/mov_bbb.mp4',
        'http://khaled-playlist-video.bitballoon.com/video/play-list/comic.mp4',
        'https://www.w3schools.com/tags/mov_bbb.mp4',
        'http://khaled-playlist-video.bitballoon.com/video/play-list/comic.mp4',
        'https://www.w3schools.com/tags/mov_bbb.mp4',
        'http://khaled-playlist-video.bitballoon.com/video/play-list/comic.mp4',
        'https://www.w3schools.com/tags/mov_bbb.mp4',
        'http://khaled-playlist-video.bitballoon.com/video/play-list/comic.mp4'
    ],
    x = 0,

    // Name Functions Contains (this)

    toggleActiveOrder,
    toggleActiveRepeat,
    updateButton,
    showVolume,
    hideVolume,
    toggleAutoplay;

// Bilud Out Function

// Set Src Video Player
video.setAttribute('src', pathPlaylist[0]);
videoMove.setAttribute('src', video.getAttribute('src'));

// Functions (Playlist)

// Update Src On Load Page
function setSrcVideo() {
    'use strict';
    var i;
    for (i = 0; i < videoPlaylist.length; i++) { // Run On load Page
        videoPlaylist[i].setAttribute('src', pathPlaylist[i]);
        countNumPlaylist[i].textContent = i + 1;
    }
}
// Functions (Playlist) Click Prev & Next
function toggleVideo(num) { // Run On Click On Forward And Backward
    'use strict';
    x = x + num;
    if (x > videoPlaylist.length - 1) { x = 0; }
    if (x < 0) { x = videoPlaylist.length - 1; }
    video.setAttribute('src', pathPlaylist[x]);
    videoMove.setAttribute('src', video.getAttribute('src'));
    video.play();
}
// Functions (Playlist) Click On Video In Playlist To Play
function clickOnLinkPlaylist() { // Run On Click On Link Playlist
    'use strict';
    var numLinks,
        funcLink = '';
    funcLink = function () {
        var srcVideoPlaylist = this.children[1].getAttribute('src');
        video.setAttribute('src', srcVideoPlaylist);
        videoMove.setAttribute('src', video.getAttribute('src'));
        video.play();
        overlayPlay.style.display = 'none';
        x = window.parseInt(this.children[0].innerHTML) - 1;
    };
    for (numLinks = 0; numLinks < linkVideo.length; numLinks++) {
        linkVideo[numLinks].onclick = funcLink;
    }
}
var vidHover = viewNext_Prev.children[0],
    titleHover = viewNext_PrevDesc.children[0],
    descHover = viewNext_PrevDesc.children[1];
function showVideoNext() {
    'use strict';
    if (x !== videoPlaylist.length - 1) {
        var hours = Math.floor(videoPlaylist[x + 1].duration / 60) / 60,
            mins = Math.floor(videoPlaylist[x + 1].duration / 60) % 60,
            seconds = Math.floor(videoPlaylist[x + 1].duration % 60);
        if (mins < 10) { mins = "0" + mins; }
        if (seconds < 10) { seconds = "0" + seconds; }
        viewNext_Prev.style.left = '95px';
        vidHover.setAttribute('src', pathPlaylist[x + 1]);
        vidHover.currentTime = videoPlaylist[x + 1].currentTime;
        titleHover.textContent = videoPlaylist[x + 1].nextElementSibling.children[0].textContent;
        descHover.textContent = videoPlaylist[x + 1].nextElementSibling.children[1].textContent;
        if (hours >= 1) {
            viewNext_PrevTime.textContent = hours + ":" + mins + ":" + seconds;
        } else {
            viewNext_PrevTime.textContent = mins + ":" + seconds;
        }
        viewNext_Prev.style.display = 'block';
    }
}
function showVideoPrev() {
    'use strict';
    if (x !== 0) {
        var hours = Math.floor(videoPlaylist[x - 1].duration / 60) / 60,
            mins = Math.floor(videoPlaylist[x - 1].duration / 60) % 60,
            seconds = Math.floor(videoPlaylist[x - 1].duration % 60);
        if (mins < 10) { mins = "0" + mins; }
        if (seconds < 10) { seconds = "0" + seconds; }
        viewNext_Prev.style.left = '55px';
        vidHover.setAttribute('src', pathPlaylist[x - 1]);
        vidHover.currentTime = videoPlaylist[x - 1].currentTime;
        titleHover.textContent = videoPlaylist[x - 1].nextElementSibling.children[0].textContent;
        descHover.textContent = videoPlaylist[x - 1].nextElementSibling.children[1].textContent;
        if (hours >= 1) {
            viewNext_PrevTime.textContent = hours + ":" + mins + ":" + seconds;
        } else {
            viewNext_PrevTime.textContent = mins + ":" + seconds;
        }
        viewNext_Prev.style.display = 'block';
    }
}
function hideVideoNext_Prev() { 'use strict'; viewNext_Prev.style.display = 'none'; }

// Functions (Playlist) Video++ when Update Time and End Video
function nextVideoAuto() { // Run When Update Time Video
    'use strict';
    if (video.ended) {
        if (btnOrder.classList.contains('active')) {
            var randomVideo = Math.floor(Math.random() * pathPlaylist.length);
            x = randomVideo;
            video.setAttribute('src', pathPlaylist[x]);
            videoMove.setAttribute('src', video.getAttribute('src'));
            video.play();
            overlayPlay.style.display = 'none';
        } else {
            if (x < videoPlaylist.length - 1) {
                x = x + 1;
                video.setAttribute('src', pathPlaylist[x]);
                videoMove.setAttribute('src', video.getAttribute('src'));
                video.play();
                overlayPlay.style.display = 'none';
            } else {
                if (btnRepeat.classList.contains('active')) {
                    x = 0;
                    overlayPlay.style.display = 'none';
                    video.setAttribute('src', pathPlaylist[x]);
                    video.play();
                } else {
                    x = videoPlaylist.length - 1;
                    overlayPlay.style.display = 'flex';
                }
            }
        }
    }
}
function hoverVideoPlaylist() {
    'use strict';
    var numVideo,
        funcOver = '',
        funcLeave = '',
        timeNow;
    funcOver = function () {
        timeNow = this.currentTime;
        this.currentTime = 0;
        if (!this) { return; }
        this.play();
    };
    funcLeave = function () {
        this.currentTime = timeNow;
        if (!this) { return; }
        this.pause();
    };
    for (numVideo = 0; numVideo < videoPlaylist.length; numVideo++) {
        timeNow = '';
        videoPlaylist[numVideo].onmouseover = funcOver;
        videoPlaylist[numVideo].onmouseleave = funcLeave;
    }
}
toggleActiveOrder = function () {
    'use strict';
    this.classList.toggle('active');
    btnRepeat.classList.remove('active');
};
toggleActiveRepeat = function () {
    'use strict';
    this.classList.toggle('active');
    btnOrder.classList.remove('active');
};
// Functions (Player) Controls
function toggleControls() { // Run On Click On Hide/Show For Toggle(show/hide) Controls
    'use strict';
    hideControl.classList.toggle('toggle');
    if (hideControl.classList.contains('toggle')) {
        hideControl.textContent = "Show";
        playerControl.classList.add('toggle');
        showTilteVideo.classList.remove('toggle');
    } else {
        hideControl.textContent = "Hide";
        playerControl.classList.remove('toggle');
        showTilteVideo.classList.add('toggle');
    }
}
function setTitleVideo() {
    'use strict';
    showTilteVideo.textContent = videoPlaylist[x].nextElementSibling.children[0].textContent;
}
function showTitleOnHoverTopVideo(e) { // Run On Move Player
    'use strict';
    var yPage = e.offsetY;
    if (document.body.classList.contains('full-screen')) {
        if (yPage <= 60) {
            showTilteVideo.classList.add('toggle');
        } else if (yPage > 60 && yPage < player.offsetWidth - 60) {
            showTilteVideo.classList.remove('toggle');
        }
    }
}
function showIconEvent() {
    'use strict';
    showElementByEvent.classList.add('toggle');
    setTimeout(function () { showElementByEvent.classList.remove('toggle'); }, 500);
}

// Functions (Player) togglePlay
function togglePlay() {
    'use strict';
    showIconEvent();
    if (video.paused) {
        video.play();
        showElementByEvent.innerHTML = '<i class="fa fa-play"></i>';
    } else {
        video.pause();
        showElementByEvent.innerHTML = '<i class="fa fa-pause"></i>';
    }
    overlayPlay.style.display = 'none';
}
updateButton = function () {
    'use strict';
    var iconFont = btnTogglePlay.children[0];
    if (this.paused) {
        iconFont.classList.add('fa-play');
        iconFont.classList.remove('fa-pause');
    } else {
        iconFont.classList.add('fa-pause');
        iconFont.classList.remove('fa-play');
    }
};

// Functions (Player) Progress
function skipPlayer(num, numSkip) { // Forward And Backward 5s for press arrow (right or left)
    'use strict';
    video.currentTime += num;
    showIconEvent();
    showElementByEvent.innerHTML = numSkip;
}
function handleProgress() { // Run On Update Time
    'use strict';
    var percent = (video.currentTime / video.duration) * 100;
    progressBar.style.flexBasis = percent + "%";
}
function moveProgress(e) { // Run On Click on Progress
    'use strict';
    var scrubTime = (e.offsetX / progress.offsetWidth) * video.duration;
    video.currentTime = scrubTime;
}
function showTimeOnMove(e) { // Run On Hover Progress
    'use strict';
    var defaultTime = (e.offsetX / progress.offsetWidth) * video.duration,
        hours = Math.floor((defaultTime / 60) / 60),
        mins = Math.floor((defaultTime / 60) % 60),
        seconds = Math.floor(defaultTime % 60);
    screenMove.style.display = 'flex';
    screenMove.style.left = e.offsetX + 'px';
    if (mins < 10) { mins = "0" + mins; }
    if (seconds < 10) { seconds = "0" + seconds; }
    if (hours < 1) {
        screenTime.textContent = mins + ":" + seconds;
    } else {
        screenTime.textContent = hours + ":" + mins + ":" + seconds;
    }
    if (e.offsetX <= 85) {
        screenMove.style.left = 85 + 'px';
    }
    if (e.offsetX >= (progress.offsetWidth - 90)) {
        screenMove.style.left = (progress.offsetWidth - 90) + 'px';
    }
    videoMove.currentTime = defaultTime;
}
function hideTimeOnMove() { // Run On outHover Progress
    'use strict';
    screenMove.style.display = 'none';
}
function showBufferedPlayer() {
    'use strict';
    if (video.buffered.length > 0) {
        var bufferedProgress = (video.buffered.end(0) / video.duration) * 100;
        progressLoad.style.width = bufferedProgress + '%';
    }
}
function overlayLoading() {
    'use strict';
    video.onwaiting = function () {
        overlayLoad.style.display = 'flex';
    };
    video.oncanplay = function () {
        overlayLoad.style.display = 'none';
    };
}

// Functions (Player) Time In Progress
function updateTime() { // Run On Update Time
    'use strict';
    var hoursTime = Math.floor((video.currentTime / 60) / 60),
        minsTime = Math.floor((video.currentTime / 60) % 60),
        secondsTime = Math.floor(video.currentTime % 60),
        hoursDuration = Math.floor((video.duration / 60) / 60),
        minsDuration = Math.floor((video.duration / 60) % 60),
        secondsDuration = Math.floor(video.duration % 60);
    if (minsTime < 10) { minsTime = "0" + minsTime; }
    if (minsDuration < 10) { minsDuration = "0" + minsDuration; }
    if (secondsTime < 10) { secondsTime = "0" + secondsTime; }
    if (secondsDuration < 10) { secondsDuration = "0" + secondsDuration; }
    if (hoursDuration < 1) {
        showTime.textContent = minsTime + ":" + secondsTime + " / " + minsDuration + ":" + secondsDuration;
    } else {
        showTime.textContent = hoursTime + ":" + minsTime + ":" + secondsTime + " / " + hoursDuration + ":" + minsDuration + ":" + secondsDuration;
    }
    if (video.currentTime === video.duration) {
        overlayPlay.style.display = 'flex';
    }
}
// Functions (Player) Volume
function handleRangeUpdate() { // Run On Change Input Range
    'use strict';
    var numRange,
        innerFun;
    innerFun = function () {
        video[this.name] = this.value;
        volumeVal = rangeVolume.value;
    };
    for (numRange = 0; numRange < ranges.length; numRange++) {
        ranges[numRange].onchange = innerFun;
        ranges[numRange].onmousemove = innerFun;
    }
}
(function () { 'use strict'; video[rangeVolume.name] = rangeVolume.value; }());

showVolume = function () { 'use strict'; this.classList.remove('toggle'); }; // Show Input On Hover
hideVolume = function () { 'use strict'; this.classList.add('toggle'); }; // Hide Input On outHover

function toggleVolume() { // Run On Click On Icon Volume For Mute Volume
    'use strict';
    buttonVolume.classList.toggle('fa-volume-up');
    buttonVolume.classList.toggle('fa-volume-off');
    if (buttonVolume.classList.contains('fa-volume-off')) {
        video.muted = true;
        rangeVolume.value = 0;
        iconCloseVolume.style.display = "block";
    } else {
        video.muted = false;
        rangeVolume.value = volumeVal;
        iconCloseVolume.style.display = "none";
        if (rangeVolume.value === 0) {
            video.volume = 0.2;
            rangeVolume.value = 0.2;
            iconCloseVolume.style.display = 'none';
            rangeVolume.onchange = function () { video.muted = false; video.volume = this.value; };
            rangeVolume.onmousemove = function () { video.muted = false; video.volume = this.value; };
        }
    }
}
if (rangeVolume.value === 0) { // Run On Load Page if Value == 0 For Mute Volume
    toggleVolume();
}
function showClassVolume() { // Run On Click On icon Volume For Toggle Icon
    'use strict';
    if (video[rangeVolume.name] === 0) {
        buttonVolume.classList.add('fa-volume-off');
        buttonVolume.classList.remove('fa-volume-up');
        iconCloseVolume.style.display = "block";
    } else {
        buttonVolume.classList.remove('fa-volume-off');
        buttonVolume.classList.add('fa-volume-up');
        iconCloseVolume.style.display = "none";
    }
}
// Functions (Player) Speed[playbackRate]
function showRate(e) { // Run On Hover On Input For Show Value Rate
    'use strict';
    viewPlayBackRate.style.display = 'block';
    if (rangePlaybackRate.value === 1) {
        viewPlayBackRate.textContent = "Normal";
        viewPlayBackRate.style.width = "80px";
        viewPlayBackRate.style.marginLeft = "-40px";
    } else {
        viewPlayBackRate.textContent = rangePlaybackRate.value;
        viewPlayBackRate.style.width = "50px";
        viewPlayBackRate.style.marginLeft = "-25px";
    }
}
function hideRate() { // Run On outHover Hide Value Rate
    'use strict';
    viewPlayBackRate.style.display = 'none';
}

// Functions (Player) Player Option
document.onclick = function () {
    'use strict';
    optionItem.parentNode.classList.remove('toggle');
};
function toggleOption(e) {
    'use strict';
    optionItem.parentNode.classList.toggle('toggle');
    e.stopPropagation();
}
function addToggleOption(e) {
    'use strict';
    optionItem.parentNode.classList.add('toggle');
    e.stopPropagation();
}
toggleAutoplay = function () { 'use strict'; this.classList.toggle('active'); };
function activeAutoPlay() {
    'use strict';
    if (autoplay.classList.contains('active')) {
        video.setAttribute('loop', 'loop');
    } else {
        video.removeAttribute('loop');
    }
}

function clickActiveQuality() {
    'use strict';
    qualityOption.parentNode.classList.toggle('active');
    qualityOptionAuto.parentNode.classList.remove('active');
}
function clickActiveAutoQuality() {
    'use strict';
    var srcVideo = video.getAttribute('src'),
        indexOfExtension = srcVideo.lastIndexOf('.'),
        extension = srcVideo.substring(indexOfExtension, srcVideo.length);
    qualityOptionAuto.parentNode.classList.toggle('active');
    qualityOption.parentNode.classList.remove('active');
    if (extension === '.mp4') {
        qualityOptionAuto.children[0].textContent = 'HD';
    } else {
        qualityOptionAuto.children[0].textContent = 'SD';
    }
}

var testPlayed = '';
video.ontimeupdate = function () {
    'use strict';
    if (video.paused) {
        testPlayed = false;
    } else { testPlayed = true; }
};
function setQuality() { // Run Function On Click Button HD
    'use strict';
    var srcVideo = video.getAttribute('src'),
        indexOfExtension = srcVideo.lastIndexOf('.'),
        extension = srcVideo.substring(indexOfExtension, srcVideo.length),
        hdVideo = encodeURI(srcVideo.replace(extension, '.mp4')),
        sdVideo = encodeURI(srcVideo.replace(extension, '.ogg'));
    timeVideoNow = video.currentTime;
    if (qualityOption.parentNode.classList.contains('active')) {
        video.setAttribute('src', hdVideo);
        video.currentTime = timeVideoNow;
        if (testPlayed === false) {
            video.pause();
        } else { video.play(); }

    } else {
        video.setAttribute('src', sdVideo);
        video.currentTime = timeVideoNow;
        if (testPlayed === false) {
            video.pause();
        } else { video.play(); }
    }
}
function checkQuality() { // Run Function On Video Time Update
    'use strict';
    var srcVideo = video.getAttribute('src'),
        indexExtension = srcVideo.lastIndexOf('.'),
        extension = srcVideo.substring(indexExtension, srcVideo.length);
    if (extension === '.mp4') {
        if (qualityOptionAuto.parentNode.classList.contains('active')) {
            qualityOptionAuto.parentNode.classList.add('active');
            qualityOptionAuto.children[0].textContent = 'HD';
        } else {
            if (extension === '.mp4') {
                qualityOption.parentNode.classList.add('active');
            } else {
                qualityOption.parentNode.classList.remove('active');
            }
        }
    } else {
        if (qualityOptionAuto.parentNode.classList.contains('active')) {
            qualityOptionAuto.parentNode.classList.add('active');
            qualityOptionAuto.children[0].textContent = 'SD';
        } else {
            qualityOption.parentNode.classList.remove('active');
        }
    }
}
function activeAutoQuality() {
    'use strict';
    var srcVideo = video.getAttribute('src'),
        indexExtension = srcVideo.lastIndexOf('.'),
        extension = srcVideo.substring(indexExtension, srcVideo.length),
        hdVideo = srcVideo.replace(extension, '.mp4'),
        sdVideo = srcVideo.replace(extension, '.ogg');
    qualityOptionAuto.parentNode.classList.add('active');
    if (video.canPlayType) {
        if (video.canPlayType('video/mp4; codecs="avc1.42E01E"') === 'probably') {
            video.setAttribute('src', hdVideo);
        } else {
            if (video.canPlayType('video/ogg; codecs="theora"') === 'probably') {
                video.setAttribute('src', sdVideo);
            }
        }
    }
    if (extension === '.mp4') {
        qualityOptionAuto.children[0].textContent = 'HD';
    } else {
        qualityOptionAuto.children[0].textContent = 'SD';
    }
}
// Functions (Player) Full Screen
function toggleFullScreen() {
    'use strict';
    btnFullScreen.children[0].classList.toggle('fa-compress');
    if (!document.fullscreenElement && !document.mozFullScreenElement && !document.webkitFullscreenElement && !document.msFullscreenElement) {
        if (player.requestFullscreen) {
            player.requestFullscreen();
        } else if (player.msRequestFullscreen) {
            player.msRequestFullscreen();
        } else if (player.mozRequestFullScreen) {
            player.mozRequestFullScreen();
        } else if (player.webkitRequestFullscreen) {
            player.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
        }
        hideControl.textContent = "Show";
        playerControl.classList.add('toggle');
        hideControl.classList.add('toggle');
        document.body.classList.add('full-screen');
    } else {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        } else if (document.msExitFullscreen) {
            document.msExitFullscreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.webkitExitFullscreen) {
            document.webkitExitFullscreen();
        }
        hideControl.textContent = "Hide";
        playerControl.classList.remove('toggle');
        hideControl.classList.remove('toggle');
        document.body.classList.remove('full-screen');
    }
}
function addFullScreen() { // Run On Press Enter For Full Screen 
    'use strict';
    btnFullScreen.children[0].classList.add('fa-compress');
    if (btnFullScreen.children[0].classList.contains('fa-compress')) {
        hideControl.textContent = "Show";
        playerControl.classList.add('toggle');
        hideControl.classList.add('toggle');
        document.body.classList.add('full-screen');
        if (!document.fullscreenElement && !document.mozFullScreenElement && !document.webkitFullscreenElement && !document.msFullscreenElement) {
            if (player.requestFullscreen) {
                player.requestFullscreen();
            } else if (player.msRequestFullscreen) {
                player.msRequestFullscreen();
            } else if (player.mozRequestFullScreen) {
                player.mozRequestFullScreen();
            } else if (player.webkitRequestFullscreen) {
                player.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
            }
        }
    }
}
function removeFullScreen() { // Run On Press ESC For Out Full Screen
    'use strict';
    if (document.body.classList.contains('full-screen')) {
        document.body.classList.remove('full-screen');
        hideControl.textContent = "Hide";
        playerControl.classList.remove('toggle');
        hideControl.classList.remove('toggle');
        btnFullScreen.children[0].classList.remove('fa-compress');
    }
}
// Hook Up the Event Listener

// Run Functions (Playlist)

// Run Functions (Playlist) Click Prev & Next
btnForward.onclick = function () { 'use strict'; toggleVideo(1); };
btnBackward.onclick = function () { 'use strict'; toggleVideo(-1); };

// Run Functions (Playlist) Hover For Show Prev & Next
btnForward.addEventListener('mouseover', showVideoNext);
btnBackward.addEventListener('mouseover', showVideoPrev);
btnForward.addEventListener('click', showVideoNext);
btnBackward.addEventListener('click', showVideoPrev);
btnForward.addEventListener('mouseleave', hideVideoNext_Prev);
btnBackward.addEventListener('mouseleave', hideVideoNext_Prev);

// Functions (Playlist) Update Current Time Video Paly List
window.onload = function () {
    // Set Source Video On Load Page 
    'use strict';
    setSrcVideo();

    var d;
    setTimeout(function () {
        for (d = 0; d < videoPlaylist.length; d++) {
            var rand = Math.random() * videoPlaylist[d].duration,
                hours = Math.floor(videoPlaylist[d].duration / 60) / 60,
                mins = Math.floor(videoPlaylist[d].duration / 60) % 60,
                seconds = Math.floor(videoPlaylist[d].duration % 60);
            videoPlaylist[d].currentTime = rand;
            if (mins < 10) { mins = "0" + mins; }
            if (seconds < 10) { seconds = "0" + seconds; }
            if (hours >= 1) {
                viewTimePlaylist[d].textContent = hours + ":" + mins + ":" + seconds;
            } else {
                viewTimePlaylist[d].textContent = mins + ":" + seconds;
            }
        }
    }, 2500);

    updateTime();
    // Run Functions (Playlist) Hover Video 
    hoverVideoPlaylist();
    // Run Functions (Playlist) Click On Links Playlist
    clickOnLinkPlaylist();
    // Run On Load Page (Active Click Auto)
    activeAutoQuality();
    // Show overlay Loading
    overlayLoading();
    handleRangeUpdate();
};

// Functions (Playlist) Click On Video In Playlist
btnOrder.addEventListener('click', toggleActiveOrder);
btnRepeat.addEventListener('click', toggleActiveRepeat);

// Run Functions (Player)

// Run Functions (Player) toggle[show/hide] Controls
hideControl.addEventListener('click', toggleControls);

// Run Functions (Player) togglePlay
window.onkeydown = function (e) {
    'use strict';
    if (e.keyCode === 32 || e.keyCode === 179) { togglePlay(); }
    if (e.keyCode === 39) { skipPlayer(5, '+5'); }
    if (e.keyCode === 37) { skipPlayer(-5, '-5'); }
    // Run Functions (Player) Full Screen
    if (e.keyCode === 13) { addFullScreen(); }
    if (e.keyCode === 27) { removeFullScreen(); }
};
player.onkeydown = function (e) { 'use strict'; if (e.keyCode === 32 || e.keyCode === 179) { togglePlay(); } };
player.addEventListener('mousemove', showTitleOnHoverTopVideo);
btnTogglePlay.addEventListener('click', togglePlay);
overlayPlay.addEventListener('click', togglePlay);
video.addEventListener('click', togglePlay);
video.addEventListener('play', updateButton);
video.addEventListener('pause', updateButton);
showElementByEvent.addEventListener('dblclick', toggleFullScreen);
// Run Functions (Player) Progress
progress.addEventListener('click', moveProgress);
progress.addEventListener('mousemove', showTimeOnMove);
progress.addEventListener('mouseout', hideTimeOnMove);
video.ontimeupdate = function () {
    'use strict';
    handleProgress();
    updateTime();
    nextVideoAuto();
    activeAutoPlay();
    checkQuality();
    setTitleVideo();
};
video.addEventListener('progress', showBufferedPlayer);
video.addEventListener('loadeddate', showBufferedPlayer);
video.addEventListener('playing', showBufferedPlayer);
video.addEventListener('cantypethrough', showBufferedPlayer);
// Run Functions (Player) Volume
rangeVolume.addEventListener('change', showClassVolume);
rangeVolume.addEventListener('mousedown', showClassVolume);
volume.addEventListener('mouseover', showVolume);
volume.addEventListener('mouseleave', hideVolume);
iconVolume.addEventListener('click', toggleVolume);
// Run Functions (Player) Speed[playbackRate]
playBackRate.addEventListener('mousemove', showRate);
playBackRate.addEventListener('mouseout', hideRate);
// Run Functions (Player) Player Option
btnOptionPlayer.addEventListener('click', toggleOption);
autoplay.addEventListener('click', toggleAutoplay);
optionItem.addEventListener('click', addToggleOption);
qualityOption.onclick = function () { 'use strict'; clickActiveQuality(); setQuality(); };
qualityOptionAuto.onclick = function () { 'use strict'; clickActiveAutoQuality(); };
// Run Functions (Player) Full Screen
video.addEventListener('dblclick', toggleFullScreen);
btnFullScreen.addEventListener('click', toggleFullScreen);


$(function () {
    $(".player-play-list .play-list ul").niceScroll({
        cursorcolor: '#777',
        background: '#CCC',
        cursorwidth: '10px',
        cursorborder: '0',
        cursorborderradius: '1px',
        autohidemode: true,
        railalign: 'left'
    });
});