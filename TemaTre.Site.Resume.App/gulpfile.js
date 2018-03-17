var gulp = require('gulp'),
    watch = require('gulp-watch'),
    prefixer = require('gulp-autoprefixer'),
    uglify = require('gulp-uglify'),
    sass = require('gulp-sass'),
    sourcemaps = require('gulp-sourcemaps'),
    rigger = require('gulp-rigger'),
    cssmin = require('gulp-minify-css'),
    imagemin = require('gulp-imagemin'),
    pngquant = require('imagemin-pngquant'),
    rimraf = require('rimraf'),
    browserSync = require("browser-sync"),
	copy = require('copy'),
    reload = browserSync.reload;
	
var argv = require('yargs')
			.default('baseDir', 'build')
			.argv;
	


var path = {
    build: { 
        html: argv.baseDir,
        js: argv.baseDir + '/js/',
        css: argv.baseDir + '/css/',
        img: argv.baseDir + '/img/',
        fonts: argv.baseDir + '/font/',
		resume: argv.baseDir  
	},
    sources: { 
        html: 'sources/*.html', 
        js: 'sources/js/*.js',
        style: 'sources/css/**/*.css',
        img: 'sources/img/**/*.*', 
        fonts: 'sources/font/**/*.*',
		resume : '.Net_Developer_En_Artem_Tregubov.pdf'
    },
    watch: { 
        html: 'sources/**/*.html',
        js: 'sources/js/**/*.js',
        style: 'sources/style/**/*.css',
        img: 'sources/img/**/*.*',
        fonts: 'sources/font/**/*.*'
    },
    clean: argv.baseDir
};

var config = {
    server: {
        baseDir: "./build"
    },
    tunnel: true,
    host: 'localhost',
    port: 9000,
    logPrefix: "Frontend_Devil"
};

gulp.task('html:build', function () {
    gulp.src(path.sources.html) 
        .pipe(rigger())
        .pipe(gulp.dest(path.build.html))
        .pipe(reload({stream: true}));
});


gulp.task('js:build', function () {
    gulp.src(path.sources.js) 
        .pipe(rigger()) 
        .pipe(sourcemaps.init()) 
        .pipe(uglify()) 
        .pipe(sourcemaps.write()) 
        .pipe(gulp.dest(path.build.js))
        .pipe(reload({stream: true}));
});

gulp.task('style:build', function () {
    gulp.src(path.sources.style) 
        .pipe(sourcemaps.init()) 
        .pipe(sass()) 
        .pipe(prefixer()) 
        .pipe(cssmin()) 
        .pipe(sourcemaps.write())
        .pipe(gulp.dest(path.build.css)) 
        .pipe(reload({stream: true}));
});

gulp.task('fonts:build', function() {
    gulp.src(path.sources.fonts)
        .pipe(gulp.dest(path.build.fonts))
});

gulp.task('resume:build', function(cb) {
    copy(path.sources.resume, path.build.resume, cb);
});

gulp.task('img:build', function () {
    gulp.src(path.sources.img) 
        .pipe(imagemin({ 
            progressive: true,
            svgoPlugins: [{removeViewBox: false}],
            use: [pngquant()],
            interlaced: true
        }))
        .pipe(gulp.dest(path.build.img)) 
        .pipe(reload({stream: true}));
});


gulp.task('build', [
    'html:build',
    'js:build',
    'style:build',
    'fonts:build',
	'img:build',
	'resume:build'
]);

gulp.task('clean', function (cb) {
    rimraf(path.clean, cb);
});

gulp.task('watch', function(){
    watch([path.watch.html], function(event, cb) {
        gulp.start('html:build');
    });
    watch([path.watch.style], function(event, cb) {
        gulp.start('style:build');
    });
    watch([path.watch.js], function(event, cb) {
        gulp.start('js:build');
    });
    watch([path.watch.img], function(event, cb) {
        gulp.start('image:build');
    });
    watch([path.watch.fonts], function(event, cb) {
        gulp.start('fonts:build');
    });
});


gulp.task('webserver', function () {
    browserSync(config);
});


